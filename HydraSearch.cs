using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunamyLibrary
{
    public class HydraSearch
    {
        [JsonProperty("@type")]
        public required string Type { get; set; }

        [JsonProperty("hydra:template")]
        public required string Template { get; set; }

        [JsonProperty("hydra:variableRepresentation")]
        public required string VariableRepresentation { get; set; }

        [JsonProperty("hydra:mapping")]
        public required List<HydraMapping> Mapping { get; set; }
    }
}
