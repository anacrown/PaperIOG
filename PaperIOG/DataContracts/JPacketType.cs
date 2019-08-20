using System;
using Newtonsoft.Json;

namespace PaperIOG.DataContracts
{
    public enum JInfoType { StartGame, EndGame, Tick }

    internal class JPacketTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jsPacketType = (JInfoType)value;

            switch (jsPacketType)
            {
                case JInfoType.StartGame:
                    writer.WriteValue("start_game");
                    break;
                case JInfoType.EndGame:
                    writer.WriteValue("end_game");
                    break;
                case JInfoType.Tick:
                    writer.WriteValue("tick");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            switch (enumString)
            {
                case "start_game": return JInfoType.StartGame;
                case "end_game": return JInfoType.EndGame;
                case "tick": return JInfoType.Tick;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}