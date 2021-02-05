using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRestaurant.Core.Migrations
{
    public partial class GRNChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedNotes_CreatedBy",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedNotes_PurchaseOrder",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNotes_CreatedBy",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNotes_PurchaseOrderId",
                table: "GoodsReceivedNotes");

            migrationBuilder.AddColumn<string>(
                name: "ApprovalReason",
                table: "GoodsReceivedNotes",
                type: "varchar(500)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "GoodsReceivedNotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ApprovedBy",
                table: "GoodsReceivedNotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "GoodsReceivedNotes",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_ApprovedBy",
                table: "GoodsReceivedNotes",
                column: "ApprovedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_PurchaseOrderId",
                table: "GoodsReceivedNotes",
                column: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedNotes_ApprovedBy",
                table: "GoodsReceivedNotes",
                column: "ApprovedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedNotes_PurchaseOrders",
                table: "GoodsReceivedNotes",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedNotes_ApprovedBy",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_GoodsReceivedNotes_PurchaseOrders",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNotes_ApprovedBy",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropIndex(
                name: "IX_GoodsReceivedNotes_PurchaseOrderId",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropColumn(
                name: "ApprovalReason",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "GoodsReceivedNotes");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "GoodsReceivedNotes");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_CreatedBy",
                table: "GoodsReceivedNotes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceivedNotes_PurchaseOrderId",
                table: "GoodsReceivedNotes",
                column: "PurchaseOrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedNotes_CreatedBy",
                table: "GoodsReceivedNotes",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsReceivedNotes_PurchaseOrder",
                table: "GoodsReceivedNotes",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
