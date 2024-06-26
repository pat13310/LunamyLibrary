using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunamyLibrary
{
    public class HydraBase
    {       
        [JsonProperty("@id")]
        public required string Id { get; set; }

        [JsonProperty("@type")]
        public required string Type { get; set; }
    }
}
