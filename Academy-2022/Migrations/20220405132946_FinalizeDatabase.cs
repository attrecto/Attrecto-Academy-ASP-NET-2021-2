using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academy_2022.Migrations
{
    public partial class FinalizeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers");

            migrationBuilder.DropIndex(
                name: "IX_CourseUsers_UserId",
                table: "CourseUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseUsers");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers",
                columns: new[] { "UserId", "CourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_AuthorId",
                table: "Courses",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_AuthorId",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourseUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseUsers",
                table: "CourseUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_UserId",
                table: "CourseUsers",
                column: "UserId");
        }
    }
}
