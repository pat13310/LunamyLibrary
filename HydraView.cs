using Newtonsoft.Json;

namespace LunamyLibrary
{
    public class HydraView
    {
        [JsonProperty("@id")]
        public required string Id { get; set; }

        [JsonProperty("@type")]
        public required string Type { get; set; }

        [JsonProperty("hydra:first")]
        public required string First { get; set; }

        [JsonProperty("hydra:last")]
        public required string Last { get; set; }

        [JsonProperty("hydra:previous")]
        public required string Previous { get; set; }

        [JsonProperty("hydra:next")]
        public required string Next { get; set; }
    }
}
