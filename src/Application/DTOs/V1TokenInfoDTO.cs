using Nexus.Application.Services.Contracts;

namespace Nexus.Application.DTOs;

public class V1TokenInfoDTO 
{
    public required V1TokenTypes TokenType; 
    public required string TokenIdentifier;

    public static V1TokenInfoDTO Native { get; } = new V1TokenInfoDTO()
    {
        TokenType = V1TokenTypes.Native,
        TokenIdentifier = ""
    };
}
