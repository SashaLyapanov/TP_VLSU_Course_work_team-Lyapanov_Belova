using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgency_Prod.Migrations
{
    public partial class addDescInTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "tours");
        }
    }
}
