using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class UniqueSecurityNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customer_SocialSecurityNumber",
                table: "Customer",
                column: "SocialSecurityNumber",
                unique: true,
                filter: "[SocialSecurityNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_SocialSecurityNumber",
                table: "Customer");
        }
    }
}
