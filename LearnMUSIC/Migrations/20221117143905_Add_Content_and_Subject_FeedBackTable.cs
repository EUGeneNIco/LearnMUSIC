using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMUSIC.Migrations
{
    public partial class Add_Content_and_Subject_FeedBackTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Feedbacks");
        }
    }
}
