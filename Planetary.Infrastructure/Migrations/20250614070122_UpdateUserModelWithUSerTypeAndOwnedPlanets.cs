using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planetary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModelWithUSerTypeAndOwnedPlanets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "OwnedPlanetIds");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "OwnedPlanetIds",
                table: "Users",
                newName: "Role");
        }
    }
}
