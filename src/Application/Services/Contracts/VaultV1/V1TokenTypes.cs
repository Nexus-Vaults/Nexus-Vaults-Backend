namespace Nexus.Application.Services.Contracts;

public enum V1TokenTypes : byte
{
    Never = 0, //This is an error
    Native = 1,
    ERC20 = 2
}
