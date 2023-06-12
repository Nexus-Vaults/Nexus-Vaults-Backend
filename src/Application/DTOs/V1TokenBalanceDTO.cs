using Nexus.Application.Utils;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Nexus.Application.DTOs;
public class V1TokenBalanceDTO
{
    public required V1TokenInfoDTO Token { get; init; }
    [JsonConverter(typeof(BigIntegerNumberJsonConverter))]
    public required BigInteger Balance { get; init; }
}
