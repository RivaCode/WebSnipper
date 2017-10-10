using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Extensions
{
    internal static class JsonExtensions
    {
        public static JProperty ToJProperty(this StoreKey src, object content)
            => new JProperty(src.ToString(), content);
    }
}