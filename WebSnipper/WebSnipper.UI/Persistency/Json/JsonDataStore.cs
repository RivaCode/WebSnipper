using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
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

        private readonly string _rootPath;

        public JsonDataStore() => _rootPath = Path.Combine(PathUtil.ObtainStoragePath(), "watcher.json");

        public IObservable<Site> GetAll()
            => LoadAllAsync().SelectMany(
                jRoot => jRoot.Property(SITES)
                    .Values()
                    .Select(jObj => jObj.ToObject<PresistentItem>().Map(ConvertToSiteWatch())));

        public async Task SaveAsync(Site newSite)
        {
            JObject root = await LoadAllAsync();
            ((JArray)root[SITES]).Add(newSite.Map(ConvertBackFromSiteWatch()));

            await Observable
                .Using(
                    () => new JsonTextWriter(new StreamWriter(PathUtil.GetFileStream(_rootPath))),
                    jtw => root.WriteToAsync(jtw).ToObservable())
                .ToTask();
        }

        private IObservable<JObject> LoadAllAsync()
            => Observable
                .Using(
                    () => new JsonTextReader(new StreamReader(PathUtil.GetFileStream(_rootPath))),
                    jtr => JObject.LoadAsync(jtr).ToObservable())
                .Select(jObject =>
                    jObject.IfNot(
                        self => self.HasValues,
                        self => jObject.Add(SITES, new JArray())));

        private Func<PresistentItem, Site> ConvertToSiteWatch()
            => persistent =>
                persistent.Map(watchPersistent =>
                    Site.New(watchPersistent.Url)
                        .With(watchPersistent.Description));

        private Func<Site, PresistentItem> ConvertBackFromSiteWatch()
            => siteWatch => new PresistentItem
            {
                Url = siteWatch.Url,
                Description = siteWatch.Description
            };



        private static class PathUtil
        {
            public static string ObtainStoragePath()
                => Environment
                    .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    .Map(folder => $@"{folder}\WebSnipper")
                    .Tee(path => path.IfNot(Directory.Exists, p => Directory.CreateDirectory(p)));

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
            [JsonIgnore]
            public DateTime LastScan { get; set; }
        }
    }

}