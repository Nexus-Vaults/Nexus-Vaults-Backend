using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Feature_Metadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeeTokenAddress",
                table: "FeatureDeployments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "FeeTokenAmount",
                table: "FeatureDeployments",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "FeeTokenSymbol",
                table: "FeatureDeployments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBasic",
                table: "FeatureDeployments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeTokenAddress",
                table: "FeatureDeployments");

            migrationBuilder.DropColumn(
                name: "FeeTokenAmount",
                table: "FeatureDeployments");

            migrationBuilder.DropColumn(
                name: "FeeTokenSymbol",
                table: "FeatureDeployments");

            migrationBuilder.DropColumn(
                name: "IsBasic",
                table: "FeatureDeployments");
        }
    }
}
