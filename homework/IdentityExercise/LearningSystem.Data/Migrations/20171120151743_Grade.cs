namespace LearningSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Grade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "UsersCourses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "UsersCourses");
        }
    }
}
