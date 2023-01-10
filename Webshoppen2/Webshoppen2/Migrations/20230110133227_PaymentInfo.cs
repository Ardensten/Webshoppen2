using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class PaymentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "PaymentInfo");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PaymentInfo");

            migrationBuilder.RenameColumn(
                name: "Method",
                table: "PaymentInfo",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "PaymentInfoId",
                table: "OrderHistory",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentInfoId",
                table: "OrderHistory");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "PaymentInfo",
                newName: "Method");

            migrationBuilder.AddColumn<int>(
                name: "CardNumber",
                table: "PaymentInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "PaymentInfo",
                type: "int",
                nullable: true);
        }
    }
}
