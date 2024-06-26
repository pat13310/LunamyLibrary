using Newtonsoft.Json;

namespace LunamyLibrary
{
    public class HydraView: HydraBase 
    { 
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
