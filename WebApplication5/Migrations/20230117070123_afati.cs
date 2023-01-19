using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class afati : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AfatiId",
                table: "Transkripta",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Afati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hapur = table.Column<bool>(type: "bit", nullable: false),
                    Rregullt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afati", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transkripta_AfatiId",
                table: "Transkripta",
                column: "AfatiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta",
                column: "AfatiId",
                principalTable: "Afati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta");

            migrationBuilder.DropTable(
                name: "Afati");

            migrationBuilder.DropIndex(
                name: "IX_Transkripta_AfatiId",
                table: "Transkripta");

            migrationBuilder.DropColumn(
                name: "AfatiId",
                table: "Transkripta");
        }
    }
}
