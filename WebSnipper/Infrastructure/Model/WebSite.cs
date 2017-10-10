using System;
using Infrastructure.Core;
using Newtonsoft.Json;

namespace Infrastructure.Model
{
    internal class WebSite : PersistencyObject
    {
        [JsonProperty(
            Required = Required.Always,
            PropertyName = "url"
        )]
        public string Url { get; set; }

        [JsonProperty(
            Required = Required.Always,
            PropertyName = "scanned"
        )]
        public DateTime ScannedAt { get; set; }

        [JsonProperty(
            Required = Required.Always,
            PropertyName = "name"
        )]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}