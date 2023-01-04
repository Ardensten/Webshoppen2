using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class removedTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestingMigrations",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestingMigrations",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
