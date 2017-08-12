using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebSnipper.UI.Persistency.Json
{
    public static class JsonExtensions
    {
        public static IObservable<Unit> WriteJson(
            this JsonTextWriter src,
            JObject root)
        {
            src.Formatting = Formatting.Indented;;
            return root.WriteToAsync(src).ToObservable();
        }
    }
}