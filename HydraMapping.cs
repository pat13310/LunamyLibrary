using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunamyLibrary
{
    public class HydraMapping: HydraBase
    {
        [JsonProperty("variable")]
        public required string Variable { get; set; }

        [JsonProperty("property")]
        public required string Property { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

    }
}
