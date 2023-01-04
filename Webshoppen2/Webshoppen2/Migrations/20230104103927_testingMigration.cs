using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class testingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestingMigrations",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestingMigrations",
                table: "Customer");
        }
    }
}
