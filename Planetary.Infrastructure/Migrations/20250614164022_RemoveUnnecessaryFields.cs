using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planetary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnnecessaryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoritePlanets",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnedPlanetIds",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoritePlanets",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnedPlanetIds",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
