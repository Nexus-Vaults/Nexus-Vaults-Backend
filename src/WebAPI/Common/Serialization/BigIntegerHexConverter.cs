using System.Globalization;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nexus.WebAPI.Common.Serialization;

public class BigIntegerHexConverter : JsonConverter<BigInteger>
{
    public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                return new BigInteger(reader.GetInt64());
            case JsonTokenType.String:
                string? input = reader.GetString();

                if (input == null)
                {
                    throw new InvalidOperationException();
                }

                if (!BigInteger.TryParse(input, NumberStyles.AllowHexSpecifier, null, out var result))
                {
                    throw new InvalidOperationException();
                }

                return result;
            default:
                throw new InvalidOperationException();
        }

    }

    public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("X"));
    }
}
