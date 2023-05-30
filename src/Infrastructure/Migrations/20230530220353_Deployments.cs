using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Deployments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChainDeployments",
                columns: table => new
                {
                    ChainId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    ChainName = table.Column<string>(type: "text", nullable: false),
                    NexusFactoryAddress = table.Column<string>(type: "text", nullable: false),
                    PublicCatalogAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChainDeployments", x => x.ChainId);
                });

            migrationBuilder.CreateTable(
                name: "CatalogDeployments",
                columns: table => new
                {
                    Address = table.Column<string>(type: "text", nullable: false),
                    ChainId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogDeployments", x => new { x.Address, x.ChainId });
                    table.ForeignKey(
                        name: "FK_CatalogDeployments_ChainDeployments_ChainId",
                        column: x => x.ChainId,
                        principalTable: "ChainDeployments",
                        principalColumn: "ChainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureDeployments",
                columns: table => new
                {
                    Address = table.Column<string>(type: "text", nullable: false),
                    ChainId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CatalogAddress = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureDeployments", x => new { x.Address, x.ChainId, x.CatalogAddress });
                    table.ForeignKey(
                        name: "FK_FeatureDeployments_CatalogDeployments_CatalogAddress_ChainId",
                        columns: x => new { x.CatalogAddress, x.ChainId },
                        principalTable: "CatalogDeployments",
                        principalColumns: new[] { "Address", "ChainId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureDeployments_ChainDeployments_ChainId",
                        column: x => x.ChainId,
                        principalTable: "ChainDeployments",
                        principalColumn: "ChainId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogDeployments_ChainId",
                table: "CatalogDeployments",
                column: "ChainId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDeployments_CatalogAddress_ChainId",
                table: "FeatureDeployments",
                columns: new[] { "CatalogAddress", "ChainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDeployments_ChainId",
                table: "FeatureDeployments",
                column: "ChainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureDeployments");

            migrationBuilder.DropTable(
                name: "CatalogDeployments");

            migrationBuilder.DropTable(
                name: "ChainDeployments");
        }
    }
}
