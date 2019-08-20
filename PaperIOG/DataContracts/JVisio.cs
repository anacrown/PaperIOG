using System.Collections.Generic;
using Newtonsoft.Json;

namespace PaperIOG.DataContracts
{
    public class JVisio
    {
        [JsonProperty(PropertyName = "visio_version")]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "config")]
        public Dictionary<string, string> Config { get; set; }

        [JsonProperty(PropertyName = "visio_info")]
        public List<JInfo> Info { get; set; }
    }
}
