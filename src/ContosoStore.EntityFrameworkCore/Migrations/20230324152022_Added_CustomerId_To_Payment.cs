using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoStore.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerIdToPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "AppPayments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppPayments_CustomerId",
                table: "AppPayments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPayments_AppCustomers_CustomerId",
                table: "AppPayments",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPayments_AppCustomers_CustomerId",
                table: "AppPayments");

            migrationBuilder.DropIndex(
                name: "IX_AppPayments_CustomerId",
                table: "AppPayments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AppPayments");
        }
    }
}
