using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Domain.Util;
using Infrastructure.Core;
using Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Repositories
{
    internal class JsonAdapterStore : IJsonStore
    {
        private IObservable<JObject> _rootStream;
        public JsonAdapterStore()
        {
            var initialStream = Observable
                .StartAsync(ct =>
                {
                    var newObject = new JObject(
                        StoreKey.Settings.ToJProperty(TimeSpan.FromSeconds(15)),
                        StoreKey.Sites.ToJProperty(new JArray()));

                    return WriteRootAsync(newObject);
                })
                .SwitchSelect(
                    () => ReadRootAsync().ToObservable()
                        .Delay(TimeSpan.FromSeconds(1))
                        .Retry(3));

            var continuesStream = JsonWatcher.Watch()
                .Delay(TimeSpan.FromSeconds(3))
                .SwitchSelect(() => ReadRootAsync());

            _rootStream = Observable
                .StartAsync(_ => ReadRootAsync())
                .Catch(initialStream)
                .Concat(continuesStream);
        }

        public IObservable<JToken> ObserveSlicedChangesUsing(StoreKey node) 
            => _rootStream.Select(root => root[node.ToString()]);

        public async Task SaveAsync(StoreKey key, Action<JToken> changeJson)
        {
            var root = await ReadRootAsync();

            changeJson(root[key]);
            await WriteRootAsync(root);
        }

        private async Task WriteRootAsync(JObject root)
        {
            using (var streamWriter = new StreamWriter(PathUtil.GetFileStream()))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                await root.WriteToAsync(jsonWriter);
            }
        }

        private async Task<JObject> ReadRootAsync()
        {
            using (var streamReader = new StreamReader(PathUtil.GetFileStream()))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return await JObject.LoadAsync(jsonReader);
            }
        }

        private static class PathUtil
        {
            private const string FILE_NAME = "watcher.json";

            public static FileStream GetFileStream()
                => new FileStream(
                    ObtainStorageFilePath(),
                    FileMode.OpenOrCreate, FileAccess.ReadWrite);

            public static string ObtainStoragePath()
                => Environment
                    .GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                    .Map(folder => $@"{folder}\WebSnipper")
                    .Tee(folder => folder.IfNot(Directory.Exists, p => Directory.CreateDirectory(p).FullName));

            public static string ObtainStorageFilePath()
                => ObtainStoragePath()
                    .Map(folder => Path.Combine(folder, FILE_NAME));
        }

        private static class JsonWatcher
        {
            public static IObservable<Unit> Watch()
            {
                FileSystemWatcher watcher =
                    new FileSystemWatcher(PathUtil.ObtainStoragePath(), "*.json")
                    {
                        NotifyFilter = NotifyFilters.LastWrite,
                        EnableRaisingEvents = true
                    };

                return Observable
                    .FromEventPattern<FileSystemEventArgs>(watcher, nameof(watcher.Changed))
                    .Select(pattern => pattern.EventArgs)
                    .Where(fileArgs => fileArgs.FullPath == PathUtil.ObtainStorageFilePath())
                    .Select(_ => Unit.Default);
            }
        }

    }
}