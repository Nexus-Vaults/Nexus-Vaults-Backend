using Nexus.Application.DTOs;
using Nexus.Application.Services.Contracts;
using System.Numerics;

namespace Nexus.Application.Handlers.Queries;

public record VaultAssetBalanceDTO(VaultInfoDTO VaultInfo, V1TokenInfoDTO TokenInfo, BigInteger Balance);