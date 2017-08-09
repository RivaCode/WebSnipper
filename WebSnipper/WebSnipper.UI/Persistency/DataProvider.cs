using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public class DataProvider
    {
        private const string SITES = "sites";

        private readonly string _rootPath;
        

        public DataProvider()
        {
            _rootPath = Path.Combine(PathUtil.ObtainRoot(), "watcher.json");
        }

        public IObservable<SiteWatch> Get()
        {
            return Observable.Using(
                    () => new JsonTextReader(
                        new StreamReader(
                            new FileStream(_rootPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))),
                    reader => JObject.LoadAsync(reader).ToObservable())
                .SelectMany(jObj => jObj
                    .Property(SITES).Values()
                    .Select(site => site.ToObject<SiteWatchPersistent>())
                    .Select(swp => SiteWatch.New(swp.Url).ChangeDescription(swp.Description)));
        }

        private static class PathUtil
        {
            public static string ObtainRoot()
            {
                string localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string folderPath = $@"{localAppDataFolder}\WebSnipper";
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                return folderPath;
            }
        }

        private class SiteWatchPersistent
        {
            public string Url { get; set; }
            public string Description { get; set; }
            public bool IsWatched { get; set; }
        }
    }
    
}