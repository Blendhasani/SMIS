using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_Students_StudentId",
                table: "StudentTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_Teachers_TeacherId",
                table: "StudentTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Subjects_SubjectId",
                table: "SubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Students_StudentId",
                table: "Transkripta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Subjects_SubjectId",
                table: "Transkripta");

            migrationBuilder.AddColumn<int>(
                name: "FakultetiId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FakultetiId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FakultetiId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fakultetet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kodi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shkurtesa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fakultetet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_FakultetiId",
                table: "Teachers",
                column: "FakultetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FakultetiId",
                table: "Subjects",
                column: "FakultetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FakultetiId",
                table: "Students",
                column: "FakultetiId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Fakultetet_FakultetiId",
                table: "Students",
                column: "FakultetiId",
                principalTable: "Fakultetet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_Students_StudentId",
                table: "StudentTeacher",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_Teachers_TeacherId",
                table: "StudentTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Fakultetet_FakultetiId",
                table: "Subjects",
                column: "FakultetiId",
                principalTable: "Fakultetet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Subjects_SubjectId",
                table: "SubjectTeacher",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Fakultetet_FakultetiId",
                table: "Teachers",
                column: "FakultetiId",
                principalTable: "Fakultetet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta",
                column: "AfatiId",
                principalTable: "Afati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Students_StudentId",
                table: "Transkripta",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Subjects_SubjectId",
                table: "Transkripta",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Fakultetet_FakultetiId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_Students_StudentId",
                table: "StudentTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_Teachers_TeacherId",
                table: "StudentTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Fakultetet_FakultetiId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Subjects_SubjectId",
                table: "SubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeacherId",
                table: "SubjectTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Fakultetet_FakultetiId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Students_StudentId",
                table: "Transkripta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transkripta_Subjects_SubjectId",
                table: "Transkripta");

            migrationBuilder.DropTable(
                name: "Fakultetet");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_FakultetiId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_FakultetiId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Students_FakultetiId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FakultetiId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "FakultetiId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "FakultetiId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_Students_StudentId",
                table: "StudentTeacher",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_Teachers_TeacherId",
                table: "StudentTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Subjects_SubjectId",
                table: "SubjectTeacher",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeacher_Teachers_TeacherId",
                table: "SubjectTeacher",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Afati_AfatiId",
                table: "Transkripta",
                column: "AfatiId",
                principalTable: "Afati",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Students_StudentId",
                table: "Transkripta",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transkripta_Subjects_SubjectId",
                table: "Transkripta",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
