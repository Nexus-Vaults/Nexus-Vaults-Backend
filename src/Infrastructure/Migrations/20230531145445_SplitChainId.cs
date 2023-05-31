using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplitChainId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogDeployments_ChainDeployments_ChainId",
                table: "CatalogDeployments");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDeployments_CatalogDeployments_CatalogAddress_ChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDeployments_ChainDeployments_ChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureDeployments",
                table: "FeatureDeployments");

            migrationBuilder.DropIndex(
                name: "IX_FeatureDeployments_CatalogAddress_ChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropIndex(
                name: "IX_FeatureDeployments_ChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChainDeployments",
                table: "ChainDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogDeployments",
                table: "CatalogDeployments");

            migrationBuilder.DropIndex(
                name: "IX_CatalogDeployments_ChainId",
                table: "CatalogDeployments");

            migrationBuilder.DropColumn(
                name: "ChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropColumn(
                name: "ChainId",
                table: "ChainDeployments");

            migrationBuilder.DropColumn(
                name: "ChainId",
                table: "CatalogDeployments");

            migrationBuilder.AddColumn<int>(
                name: "ContractChainId",
                table: "FeatureDeployments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContractChainId",
                table: "ChainDeployments",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "EVMChainId",
                table: "ChainDeployments",
                type: "numeric(20,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VaultV1ControllerAddress",
                table: "ChainDeployments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ContractChainId",
                table: "CatalogDeployments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureDeployments",
                table: "FeatureDeployments",
                columns: new[] { "Address", "ContractChainId", "CatalogAddress" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChainDeployments",
                table: "ChainDeployments",
                column: "ContractChainId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogDeployments",
                table: "CatalogDeployments",
                columns: new[] { "Address", "ContractChainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDeployments_CatalogAddress_ContractChainId",
                table: "FeatureDeployments",
                columns: new[] { "CatalogAddress", "ContractChainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDeployments_ContractChainId",
                table: "FeatureDeployments",
                column: "ContractChainId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogDeployments_ContractChainId",
                table: "CatalogDeployments",
                column: "ContractChainId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogDeployments_ChainDeployments_ContractChainId",
                table: "CatalogDeployments",
                column: "ContractChainId",
                principalTable: "ChainDeployments",
                principalColumn: "ContractChainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDeployments_CatalogDeployments_CatalogAddress_Contra~",
                table: "FeatureDeployments",
                columns: new[] { "CatalogAddress", "ContractChainId" },
                principalTable: "CatalogDeployments",
                principalColumns: new[] { "Address", "ContractChainId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDeployments_ChainDeployments_ContractChainId",
                table: "FeatureDeployments",
                column: "ContractChainId",
                principalTable: "ChainDeployments",
                principalColumn: "ContractChainId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogDeployments_ChainDeployments_ContractChainId",
                table: "CatalogDeployments");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDeployments_CatalogDeployments_CatalogAddress_Contra~",
                table: "FeatureDeployments");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureDeployments_ChainDeployments_ContractChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeatureDeployments",
                table: "FeatureDeployments");

            migrationBuilder.DropIndex(
                name: "IX_FeatureDeployments_CatalogAddress_ContractChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropIndex(
                name: "IX_FeatureDeployments_ContractChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChainDeployments",
                table: "ChainDeployments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogDeployments",
                table: "CatalogDeployments");

            migrationBuilder.DropIndex(
                name: "IX_CatalogDeployments_ContractChainId",
                table: "CatalogDeployments");

            migrationBuilder.DropColumn(
                name: "ContractChainId",
                table: "FeatureDeployments");

            migrationBuilder.DropColumn(
                name: "ContractChainId",
                table: "ChainDeployments");

            migrationBuilder.DropColumn(
                name: "EVMChainId",
                table: "ChainDeployments");

            migrationBuilder.DropColumn(
                name: "VaultV1ControllerAddress",
                table: "ChainDeployments");

            migrationBuilder.DropColumn(
                name: "ContractChainId",
                table: "CatalogDeployments");

            migrationBuilder.AddColumn<decimal>(
                name: "ChainId",
                table: "FeatureDeployments",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChainId",
                table: "ChainDeployments",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChainId",
                table: "CatalogDeployments",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeatureDeployments",
                table: "FeatureDeployments",
                columns: new[] { "Address", "ChainId", "CatalogAddress" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChainDeployments",
                table: "ChainDeployments",
                column: "ChainId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogDeployments",
                table: "CatalogDeployments",
                columns: new[] { "Address", "ChainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDeployments_CatalogAddress_ChainId",
                table: "FeatureDeployments",
                columns: new[] { "CatalogAddress", "ChainId" });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureDeployments_ChainId",
                table: "FeatureDeployments",
                column: "ChainId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogDeployments_ChainId",
                table: "CatalogDeployments",
                column: "ChainId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogDeployments_ChainDeployments_ChainId",
                table: "CatalogDeployments",
                column: "ChainId",
                principalTable: "ChainDeployments",
                principalColumn: "ChainId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDeployments_CatalogDeployments_CatalogAddress_ChainId",
                table: "FeatureDeployments",
                columns: new[] { "CatalogAddress", "ChainId" },
                principalTable: "CatalogDeployments",
                principalColumns: new[] { "Address", "ChainId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureDeployments_ChainDeployments_ChainId",
                table: "FeatureDeployments",
                column: "ChainId",
                principalTable: "ChainDeployments",
                principalColumn: "ChainId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
