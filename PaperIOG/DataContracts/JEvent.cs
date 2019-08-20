using Newtonsoft.Json;

namespace PaperIOG.DataContracts
{
    public class JEvent
    {
        [JsonProperty(PropertyName = "tick_num")]
        public int Tick { get; set; }

        [JsonProperty(PropertyName = "event")]
        public string Event { get; set; }

        [JsonProperty(PropertyName = "player")]
        public JPlayer Player { get; set; }

        [JsonProperty(PropertyName = "other")]
        public JPlayer Other { get; set; }
    }
}