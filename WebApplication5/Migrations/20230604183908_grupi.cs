using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class grupi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rfid",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grupet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupiLenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupiId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupiLenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupiLenda_Grupet_GrupiId",
                        column: x => x.GrupiId,
                        principalTable: "Grupet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupiLenda_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupiStudentet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrupiId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupiStudentet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupiStudentet_Grupet_GrupiId",
                        column: x => x.GrupiId,
                        principalTable: "Grupet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupiStudentet_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupiLenda_GrupiId",
                table: "GrupiLenda",
                column: "GrupiId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupiLenda_SubjectId",
                table: "GrupiLenda",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupiStudentet_GrupiId",
                table: "GrupiStudentet",
                column: "GrupiId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupiStudentet_StudentId",
                table: "GrupiStudentet",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupiLenda");

            migrationBuilder.DropTable(
                name: "GrupiStudentet");

            migrationBuilder.DropTable(
                name: "Grupet");

            migrationBuilder.DropColumn(
                name: "Rfid",
                table: "Students");
        }
    }
}
