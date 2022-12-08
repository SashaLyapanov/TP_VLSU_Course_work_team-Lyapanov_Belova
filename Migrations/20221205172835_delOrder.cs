using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency_Prod.Migrations
{
    public partial class delOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_baskets_orders_OrderId",
                table: "baskets");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "baskets",
                newName: "TourId");

            migrationBuilder.RenameIndex(
                name: "IX_baskets_OrderId",
                table: "baskets",
                newName: "IX_baskets_TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_baskets_tours_TourId",
                table: "baskets",
                column: "TourId",
                principalTable: "tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
