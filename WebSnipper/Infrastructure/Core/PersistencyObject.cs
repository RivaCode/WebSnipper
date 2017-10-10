using Newtonsoft.Json;

namespace Infrastructure.Core
{
    internal class PersistencyObject
    {
        [JsonProperty(
            Required = Required.Always,
            IsReference = true,
            PropertyName = "id"
        )]
        public string Id { get; set; }
    }
}