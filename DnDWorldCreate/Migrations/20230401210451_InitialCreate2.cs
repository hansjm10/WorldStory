using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDWorldCreate.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "NPCs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Alignment",
                table: "NPCs",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "Alignment",
                table: "NPCs");
        }
    }
}
