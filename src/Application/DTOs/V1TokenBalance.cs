using System.Numerics;

namespace Nexus.Application.DTOs;
public record V1TokenBalance(V1TokenInfoDTO Token, BigInteger Balance);
