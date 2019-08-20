using System.Collections.Generic;
using Newtonsoft.Json;

namespace PaperIOG.DataContracts
{
    public class JInfo
    {
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(JPacketTypeConverter))]
        public JInfoType InfoType { get; set; }

        [JsonProperty(PropertyName = "x_cells_count")]
        public int XCellsCount { get; set; }

        [JsonProperty(PropertyName = "y_cells_count")]
        public int YCellsCount { get; set; }

        [JsonProperty(PropertyName = "speed")]
        public int Speed { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        [JsonProperty(PropertyName = "max_execution_time")]
        public int MaxExecutionTime { get; set; }

        [JsonProperty(PropertyName = "request_max_time")]
        public int RequestMaxTime { get; set; }

        [JsonProperty(PropertyName = "max_tick_count")]
        public int MaxTickCount { get; set; }

        [JsonProperty(PropertyName = "tick_num")]
        public int Tick { get; set; }

        [JsonProperty(PropertyName = "players")]
        public Dictionary<string, JPlayer> Players { get; set; }

        [JsonProperty(PropertyName = "bonuses")]
        public List<JBonus> Bonuses { get; set; }

        [JsonProperty(PropertyName = "events")]
        public List<JEvent> Events { get; set; }

        [JsonProperty(PropertyName = "scores")]
        public Dictionary<string, int> Scores { get; set; }
    }
}