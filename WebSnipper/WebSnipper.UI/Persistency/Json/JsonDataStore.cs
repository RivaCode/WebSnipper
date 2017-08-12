using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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
        private const string SITES = "sites";
        private const string REFRESH = "refresh";

        private readonly string _rootPath;

        public JsonDataStore() => _rootPath = Path.Combine(PathUtil.ObtainStoragePath(), "watcher.json");

        public IObservable<Site> GetAllSites()
            => LoadAllAsync().SelectMany(
                jRoot => jRoot.Property(SITES)
                    .Values()
                    .Select(jObj => jObj.ToObject<PresistentItem>().Map(ConvertToSiteWatch())));

        public async Task SaveAsync(Site newSite)
        {
            JObject root = await LoadAllAsync();
            ((JArray)root[SITES]).Add(JObject.FromObject(newSite.Map(ConvertBackToPersisten())));

            await Observable.Using(
                    () => new JsonTextWriter(new StreamWriter(PathUtil.GetFileStream(_rootPath))),
                    jtw => jtw.WriteJson(root))
                .ToTask();
        }

        private IObservable<JObject> LoadAllAsync()
        {
            return Observable.Using(
                    () => new JsonTextReader(new StreamReader(PathUtil.GetFileStream(_rootPath))),
                    jtr => JObject.LoadAsync(jtr).ToObservable())
                .Catch((Exception e) =>
                    Observable.Start(
                            () => new JObject(
                                new JProperty(REFRESH, TimeSpan.FromMinutes(15)),
                                new JProperty(SITES, new JArray())))
                        .Delay(TimeSpan.FromSeconds(3))
                        .Select(jObj =>
                            Observable.Using(
                                () => new JsonTextWriter(new StreamWriter(PathUtil.GetFileStream(_rootPath))),
                                jtw => jtw.WriteJson(jObj).Select(_ => jObj)))
                        .Switch());
        }

        private Func<PresistentItem, Site> ConvertToSiteWatch()
            => item =>
                item.Map(watchPersistent =>
                    Site.New(watchPersistent.Url)
                        .With(watchPersistent.Description)
                        .With(item.LastScan));

        private Func<Site, PresistentItem> ConvertBackToPersisten()
            => site => new PresistentItem
            {
                Url = site.Url,
                Description = site.Description,
                LastScan = site.LastWatchTime
            };
                

        private static class PathUtil
        {
            public static string ObtainStoragePath()
                => Environment
                    .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    .Map(folder => $@"{folder}\WebSnipper")
                    .Tee(path => path.IfNot(Directory.Exists, p => Directory.CreateDirectory(p).FullName));

            public static FileStream GetFileStream(string path)
                => new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class PresistentItem
        {
            public string Url { get; set; }
            public string Description { get; set; }
            [JsonIgnore]
            public bool IsWatched { get; set; }
            public DateTime LastScan { get; set; }
        }
    }

}