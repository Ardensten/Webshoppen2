using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class CheckoutCartOrderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CheckoutCartOrderId",
                table: "OrderHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CheckoutCartOrderId",
                table: "OrderHistory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
