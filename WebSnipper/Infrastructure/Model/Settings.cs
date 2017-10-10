using System;
using Infrastructure.Core;
using Newtonsoft.Json;

namespace Infrastructure.Model
{
    class Settings : PersistencyObject
    {
        [JsonProperty(
            Required = Required.Default
        )]
        public DateTime RefreshSites { get; set; }
    }
}
