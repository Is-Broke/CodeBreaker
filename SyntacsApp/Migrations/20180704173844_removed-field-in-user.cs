using Microsoft.EntityFrameworkCore.Migrations;

namespace SyntacsApp.Migrations
{
    public partial class removedfieldinuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrExampleID",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ErrExampleID",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }
    }
}
