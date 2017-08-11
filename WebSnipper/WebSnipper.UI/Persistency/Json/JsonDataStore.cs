using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
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

        public IObservable<SiteWatch> GetAll()
            => Observable
                .Using(CreateReader(), jtr => JObject.LoadAsync(jtr).ToObservable())
                .SelectMany(
                    jObj => jObj.Property(SITES)
                        .Values()
                        .Select(site => site.ToObject<SiteWatchPersistent>().Map(ReplaceWithSiteWatch())))
                .SelectMany(
                    (watch, index) => Observable.Start(() => watch).Delay(TimeSpan.FromSeconds(index)));

        private Func<JsonTextReader> CreateReader()
            => () => new JsonTextReader(
                     new StreamReader(new FileStream(_rootPath, FileMode.OpenOrCreate, FileAccess.ReadWrite)));

        private Func<SiteWatchPersistent, SiteWatch> ReplaceWithSiteWatch()
            => persistent =>
                persistent.Map(watchPersistent =>
                    SiteWatch
                        .New(watchPersistent.Url)
                        .ChangeDescription(watchPersistent.Description));

        private static class PathUtil
        {
            public static string ObtainStoragePath()
                => Environment
                    .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    .Map(folder => $@"{folder}\WebSnipper")
                    .Tee(path => path.IfNot(Directory.Exists, p => Directory.CreateDirectory(p)));
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        private class SiteWatchPersistent
        {
            public string Url { get; set; }
            public string Description { get; set; }
            public bool IsWatched { get; set; }
        }
    }

}