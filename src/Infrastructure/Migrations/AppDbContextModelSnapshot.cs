﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nexus.Infrastructure.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.CatalogDeployment", b =>
                {
                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<decimal>("ChainId")
                        .HasColumnType("numeric(20,0)");

                    b.HasKey("Address", "ChainId");

                    b.HasIndex("ChainId");

                    b.ToTable("CatalogDeployments", (string)null);
                });

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.ChainDeployment", b =>
                {
                    b.Property<decimal>("ChainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("ChainName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NexusFactoryAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PublicCatalogAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RpcUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ChainId");

                    b.ToTable("ChainDeployments", (string)null);
                });

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.FeatureDeployment", b =>
                {
                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<decimal>("ChainId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("CatalogAddress")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FeeTokenAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("FeeTokenAmount")
                        .HasColumnType("numeric(20,0)");

                    b.Property<string>("FeeTokenSymbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBasic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Address", "ChainId", "CatalogAddress");

                    b.HasIndex("ChainId");

                    b.HasIndex("CatalogAddress", "ChainId");

                    b.ToTable("FeatureDeployments", (string)null);
                });

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.CatalogDeployment", b =>
                {
                    b.HasOne("Nexus.Domain.Entities.Deployment.ChainDeployment", "Chain")
                        .WithMany("Catalogs")
                        .HasForeignKey("ChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chain");
                });

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.FeatureDeployment", b =>
                {
                    b.HasOne("Nexus.Domain.Entities.Deployment.ChainDeployment", "Chain")
                        .WithMany("Features")
                        .HasForeignKey("ChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nexus.Domain.Entities.Deployment.CatalogDeployment", "Catalog")
                        .WithMany("Features")
                        .HasForeignKey("CatalogAddress", "ChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");

                    b.Navigation("Chain");
                });

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.CatalogDeployment", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("Nexus.Domain.Entities.Deployment.ChainDeployment", b =>
                {
                    b.Navigation("Catalogs");

                    b.Navigation("Features");
                });
#pragma warning restore 612, 618
        }
    }
}
