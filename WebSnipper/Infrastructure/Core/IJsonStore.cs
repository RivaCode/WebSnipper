using System;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Core
{
    internal interface IJsonStore
    {
        IObservable<JToken> ObserveSlicedChangesUsing(StoreKey node);

        Task SaveAsync(StoreKey key, Action<JToken> changeJson);
    }
}
