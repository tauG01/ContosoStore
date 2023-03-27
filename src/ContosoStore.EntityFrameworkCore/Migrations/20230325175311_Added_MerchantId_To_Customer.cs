using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoStore.Migrations
{
    /// <inheritdoc />
    public partial class AddedMerchantIdToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MerchantId",
                table: "AppCustomers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomers_MerchantId",
                table: "AppCustomers",
                column: "MerchantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomers_AppMerchants_MerchantId",
                table: "AppCustomers",
                column: "MerchantId",
                principalTable: "AppMerchants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomers_AppMerchants_MerchantId",
                table: "AppCustomers");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomers_MerchantId",
                table: "AppCustomers");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                table: "AppCustomers");
        }
    }
}
