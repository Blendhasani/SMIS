using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class afatitranskripta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta");

            migrationBuilder.DropIndex(
                name: "IX_Transkripta_AfatiId",
                table: "Transkripta");

            migrationBuilder.DropColumn(
                name: "AfatiId",
                table: "Transkripta");

            migrationBuilder.RenameColumn(
                name: "Rregullt",
                table: "Afati",
                newName: "VitiAkademik");

            migrationBuilder.AddColumn<string>(
                name: "Emri",
                table: "Afati",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NrProvimeve",
                table: "Afati",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AfatiTranskripta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AfatiId = table.Column<int>(type: "int", nullable: false),
                    TranskriptaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfatiTranskripta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AfatiTranskripta_Afati_AfatiId",
                        column: x => x.AfatiId,
                        principalTable: "Afati",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AfatiTranskripta_Transkripta_TranskriptaId",
                        column: x => x.TranskriptaId,
                        principalTable: "Transkripta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AfatiTranskripta_AfatiId",
                table: "AfatiTranskripta",
                column: "AfatiId");

            migrationBuilder.CreateIndex(
                name: "IX_AfatiTranskripta_TranskriptaId",
                table: "AfatiTranskripta",
                column: "TranskriptaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfatiTranskripta");

            migrationBuilder.DropColumn(
                name: "Emri",
                table: "Afati");

            migrationBuilder.DropColumn(
                name: "NrProvimeve",
                table: "Afati");

            migrationBuilder.RenameColumn(
                name: "VitiAkademik",
                table: "Afati",
                newName: "Rregullt");

            migrationBuilder.AddColumn<int>(
                name: "AfatiId",
                table: "Transkripta",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
