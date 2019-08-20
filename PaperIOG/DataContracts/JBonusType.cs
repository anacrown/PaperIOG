using System;
using Newtonsoft.Json;

namespace PaperIOG.DataContracts
{
    public enum JBonusType { Flash, Explorer, Saw }

    internal class JBonusTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jsBonusType = (JBonusType)value;

            switch (jsBonusType)
            {
                case JBonusType.Flash:
                    writer.WriteValue("n");
                    break;
                case JBonusType.Explorer:
                    writer.WriteValue("s");
                    break;
                case JBonusType.Saw:
                    writer.WriteValue("saw");
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
                case "n": return JBonusType.Flash;
                case "s": return JBonusType.Explorer;
                case "saw": return JBonusType.Saw;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}