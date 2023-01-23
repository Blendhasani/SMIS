using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class AddSubject_Shtesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kategoria",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Kodi",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KoheZgjatja",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameTranslated",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Niveli",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kategoria",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Kodi",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "KoheZgjatja",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "NameTranslated",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Niveli",
                table: "Subjects");
        }
    }
}
