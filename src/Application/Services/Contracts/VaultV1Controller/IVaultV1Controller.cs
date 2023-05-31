using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Application.Services.Contracts;
public interface IVaultV1Controller
{
    public Task<VaultInfoDTO[]> GetVaultsAsync(byte[] nexusId);
}
