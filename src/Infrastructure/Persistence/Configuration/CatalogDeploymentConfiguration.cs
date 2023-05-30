using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Infrastructure.Persistence.Configuration;
public class CatalogDeploymentConfiguration : IEntityTypeConfiguration<CatalogDeployment>
{
    public void Configure(EntityTypeBuilder<CatalogDeployment> b)
    {
        b.Property(x => x.Address);
        b.HasKey(x => new { x.Address, x.ChainId });

        b.Property(x => x.ChainId);

        b.HasMany(x => x.Features)
         .WithOne(x => x.Catalog)
         .HasForeignKey(x => new { x.CatalogAddress, x.ChainId })
         .OnDelete(DeleteBehavior.Cascade);

        b.ToTable("CatalogDeployments");
    }
}
