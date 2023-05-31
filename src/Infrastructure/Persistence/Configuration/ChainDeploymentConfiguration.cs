using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Infrastructure.Persistence.Configuration;
public class ChainDeploymentConfiguration : IEntityTypeConfiguration<ChainDeployment>
{
    public void Configure(EntityTypeBuilder<ChainDeployment> b)
    {
        b.Property(x => x.ContractChainId);
        b.HasKey(x => x.ContractChainId);

        b.Property(x => x.EVMChainId)
         .IsRequired(false);

        b.Property(x => x.ChainName);

        b.Property(x => x.NexusFactoryAddress);
        b.Property(x => x.VaultV1ControllerAddress);
        b.Property(x => x.PublicCatalogAddress);

        b.HasMany(x => x.Catalogs)
         .WithOne(x => x.Chain)
         .HasForeignKey(x => x.ContractChainId)
         .OnDelete(DeleteBehavior.Cascade);

        b.HasMany(x => x.Features)
         .WithOne(x => x.Chain)
         .HasForeignKey(x => x.ContractChainId)
         .OnDelete(DeleteBehavior.Cascade);

        b.ToTable("ChainDeployments");
    }
}
