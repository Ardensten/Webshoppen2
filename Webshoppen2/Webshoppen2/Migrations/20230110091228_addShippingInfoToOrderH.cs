using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class addShippingInfoToOrderH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ShippingInfo");

            migrationBuilder.AddColumn<int>(
                name: "ShippingInfoId",
                table: "OrderHistory",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingInfoId",
                table: "OrderHistory");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ShippingInfo",
                type: "int",
                nullable: true);
        }
    }
}
