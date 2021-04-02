using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRestaurant.Core.Migrations
{
    public partial class PurchaseOrderNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "Suppliers",
                type: "varchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalReason",
                table: "PurchaseOrders",
                type: "varchar(500)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalReason",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Address2",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldNullable: true);
        }
    }
}
