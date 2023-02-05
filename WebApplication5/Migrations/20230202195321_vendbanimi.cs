using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class vendbanimi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Residences_ResidenceId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_Students_ResidenceId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ResidenceId",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "Residence",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Residence",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "ResidenceId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Kodi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residences_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ResidenceId",
                table: "Students",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_StateId",
                table: "Residences",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Residences_ResidenceId",
                table: "Students",
                column: "ResidenceId",
                principalTable: "Residences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
