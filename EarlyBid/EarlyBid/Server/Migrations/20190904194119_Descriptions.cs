using Microsoft.EntityFrameworkCore.Migrations;

namespace EarlyBid.Server.Migrations
{
    public partial class Descriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bids",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Auction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Auction");
        }
    }
}
