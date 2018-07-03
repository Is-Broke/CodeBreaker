using Microsoft.EntityFrameworkCore.Migrations;

namespace SyntacsApp.Migrations
{
    public partial class addedupvoteproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpVote",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpVote",
                table: "Comments");
        }
    }
}
