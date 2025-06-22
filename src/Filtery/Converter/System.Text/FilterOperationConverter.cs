using Filtery.Models.Filter;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filtery.Converter.System.Text
{
    public class FilterOperationConverter : JsonConverter<FilterOperation>
    {
        public override FilterOperation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var enumValue = reader.GetString();

                if (Enum.TryParse(enumValue, ignoreCase: true, out FilterOperation result))
                {
                    return result;
                }
            }

            throw new JsonException($"Unable to convert \"{reader.GetString()}\" to {typeof(FilterOperation)}");
        }

        public override void Write(Utf8JsonWriter writer, FilterOperation value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
