﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexus.Domain.Entities.Deployment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Infrastructure.Persistence.Configuration;
public class FeatureDeploymentConfiguration : IEntityTypeConfiguration<FeatureDeployment>
{
    public void Configure(EntityTypeBuilder<FeatureDeployment> b)
    {
        b.Property(x => x.Address);
        b.HasKey(x => new { x.Address, x.ChainId, x.CatalogAddress });

        b.Property(x => x.Name);
        b.Property(x => x.Description);

        b.Property(x => x.ChainId);
        b.Property(x => x.CatalogAddress);

        b.ToTable("FeatureDeployments");
    }
}
