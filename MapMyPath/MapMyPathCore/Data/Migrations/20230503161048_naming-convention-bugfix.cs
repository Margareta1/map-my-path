using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapMyPathCore.Data.Migrations
{
    public partial class namingconventionbugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Coordinate",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Coordinate",
                newName: "Latitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Coordinate",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Coordinate",
                newName: "latitude");
        }
    }
}
