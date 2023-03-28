using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDWorldCreate.Migrations
{
    /// <inheritdoc />
    public partial class StatsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "NPCs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatsId",
                table: "NPCs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BaseStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Constitution = table.Column<int>(type: "int", nullable: false),
                    Intelligence = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<int>(type: "int", nullable: false),
                    Dexterity = table.Column<int>(type: "int", nullable: false),
                    Wisdom = table.Column<int>(type: "int", nullable: false),
                    Charisma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NPCs_StatsId",
                table: "NPCs",
                column: "StatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NPCs_BaseStats_StatsId",
                table: "NPCs",
                column: "StatsId",
                principalTable: "BaseStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NPCs_BaseStats_StatsId",
                table: "NPCs");

            migrationBuilder.DropTable(
                name: "BaseStats");

            migrationBuilder.DropIndex(
                name: "IX_NPCs_StatsId",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "NPCs");

            migrationBuilder.DropColumn(
                name: "StatsId",
                table: "NPCs");
        }
    }
}
