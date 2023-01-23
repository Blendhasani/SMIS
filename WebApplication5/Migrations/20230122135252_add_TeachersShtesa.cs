using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class add_TeachersShtesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Angazhimi",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Angazhuar",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NrPersonal",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitulliShkencor",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Angazhimi",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Angazhuar",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "NrPersonal",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "TitulliShkencor",
                table: "Teachers");
        }
    }
}
