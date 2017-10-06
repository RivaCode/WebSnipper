using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSnipper.UI.Core;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency.Json
{
    public class JsonDataStore : IDataStore
    {
        private JsonWatcher _jsonWatcher;
        private const string SITES = "sites";
        private const string REFRESH = "refresh";

        public JsonDataStore() => _jsonWatcher = new JsonWatcher();

        public IObservable<Site> GetAllSites()
            => _jsonWatcher
                .JsonChanged
                .SelectMany(
                    jRoot => jRoot.Property(SITES)
                        .Values()
                        .Select(jObj => jObj.ToObject<SitePresistentItem>().Map(Persistent.ConvertToSiteWatch())));

        public async Task SaveSiteAsync(Site newSite)
        {
            await _jsonWatcher
                .JsonChanged
                .Do(root => ((JArray) root[SITES]).Add(
                    JObject.FromObject(newSite.Map(Persistent.ConvertBackToPersisten()))))
                .Select(root => _jsonWatcher.SaveRootAsync(root))
                .ToTask();
        }

        public async Task UpdateSiteIsChanged(SiteUpdate updateInfo)
        {
            JObject root = await _jsonWatcher.JsonChanged;
            root.Property(SITES).Values();
        }

        public IObservable<RefreshRate> GetRefershRate()
            => _jsonWatcher
                .JsonChanged
                .Select(root => root.Property(REFRESH).ToObject<TimeSpan>())
                .Select(time => new RefreshRate(time));

        private static class Persistent
        {
            public static Func<SitePresistentItem, Site> ConvertToSiteWatch()
                => item =>
                    item.Map(watchPersistent =>
                        Site.New(watchPersistent.Url)
                            .With(watchPersistent.Description)
                            .With(item.LastScan));

            public static Func<Site, SitePresistentItem> ConvertBackToPersisten()
                => site => new SitePresistentItem
                {
                    Url = site.Url,
                    Description = site.Description,
                    LastScan = site.LastWatchTime
                };
        }
        private static class PathUtil
        {
            public static string FileName => "watcher.json";
            public static FileStream GetFileStream()
                => new FileStream(ObtainStorageFilePath(), FileMode.OpenOrCreate, FileAccess.ReadWrite);

            public static string ObtainStoragePath()
                => Environment
                    .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    .Map(folder => $@"{folder}\WebSnipper")
                    .Tee(folder => folder.IfNot(Directory.Exists, p => Directory.CreateDirectory(p).FullName));

            public static string ObtainStorageFilePath()
                => ObtainStoragePath()
                    .Map(folder => Path.Combine(folder, FileName));
        }

        private class JsonWatcher
        {
            private readonly BehaviorSubject<JObject> _rootSubject;
            public IObservable<JObject> JsonChanged => _rootSubject.SkipWhile(root => root == null).AsObservable();
            public JsonWatcher()
            {
                _rootSubject = new BehaviorSubject<JObject>(null);
                FileSystemWatcher watcher =
                    new FileSystemWatcher(PathUtil.ObtainStoragePath(), "*.json")
                    {
                        NotifyFilter = NotifyFilters.LastWrite
                    };

                ObserveRootLoad()
                    .Concat(
                        Observable
                            .FromEventPattern<FileSystemEventArgs>(watcher, nameof(watcher.Changed))
                            .Select(pattern => pattern.EventArgs)
                            .Where(fileArgs => fileArgs.FullPath == PathUtil.ObtainStorageFilePath())
                            .Delay(TimeSpan.FromSeconds(5))
                            .SwitchSelect(_ => ObserveRootLoad())
                    )
                    .Subscribe(root => _rootSubject.OnNext(root));

                watcher.EnableRaisingEvents = true;
            }

            public Task SaveRootAsync(JObject root)
                => Observable
                    .Using(
                        () => new JsonTextWriter(new StreamWriter(PathUtil.GetFileStream())),
                        jtw => jtw.WriteJson(root))
                    .ToTask();

            private IObservable<JObject> ObserveRootLoad()
                => Observable.Using(
                        () => new JsonTextReader(new StreamReader(PathUtil.GetFileStream())),
                        jtr => JObject.LoadAsync(jtr).ToObservable())
                    .Catch((Exception e) =>
                        Observable.Start(
                                () => new JObject(
                                    new JProperty(REFRESH, TimeSpan.FromSeconds(15)),
                                    new JProperty(SITES, new JArray())))
                            .Delay(TimeSpan.FromSeconds(3)) //Let the previous resource to be released
                            .SwitchSelect(jObj =>
                                Observable.Using(
                                    ct => Task.Run(() => new JsonTextWriter(new StreamWriter(PathUtil.GetFileStream())), ct),
                                    (jtw, ct) => Task.Run(() => jtw.WriteJson(jObj).Select(_ => jObj), ct)))
                            .Delay(TimeSpan.FromSeconds(3)));
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class SitePresistentItem
        {
            public string Url { get; set; }
            public string Description { get; set; }
            public DateTime LastScan { get; set; }
        }
    }

}