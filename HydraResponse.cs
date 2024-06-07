using Newtonsoft.Json;

namespace LunamyLibrary
{
    public class HydraResponse<T>
    {
        [JsonProperty("hydra:member")]
        public required List<T> Members { get; set; }

        [JsonProperty("hydra:totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("hydra:view")]
        public required HydraView View { get; set; }

        [JsonProperty("hydra:search")]
        public required HydraSearch Search { get; set; }
    }
    
}
