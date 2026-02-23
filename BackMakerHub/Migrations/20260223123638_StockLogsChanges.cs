using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackMakerHub.Migrations
{
    /// <inheritdoc />
    public partial class StockLogsChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityAdded",
                table: "StockLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockLogs_ProductId",
                table: "StockLogs",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockLogs_Products_ProductId",
                table: "StockLogs",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockLogs_Products_ProductId",
                table: "StockLogs");

            migrationBuilder.DropIndex(
                name: "IX_StockLogs_ProductId",
                table: "StockLogs");

            migrationBuilder.DropColumn(
                name: "QuantityAdded",
                table: "StockLogs");
        }
    }
}
