using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nexus.Application.Utils;

public class BigIntegerNumberJsonConverter : JsonConverter<BigInteger>
{
    public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? jsonValue = reader.GetString();

        return jsonValue is null 
            ? throw new JsonException("null is not a valid value for BigInteger") 
            : BigInteger.Parse(jsonValue);
    }

    public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options)
    {
        string jsonValue = value.ToString();
        writer.WriteStringValue(jsonValue);
    }
}