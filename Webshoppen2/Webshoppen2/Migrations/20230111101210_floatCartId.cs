using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webshoppen2.Migrations
{
    public partial class floatCartId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CheckoutCartOrderId",
                table: "OrderHistory",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CheckoutCartOrderId",
                table: "OrderHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
