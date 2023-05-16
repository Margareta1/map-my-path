using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapMyPathCore.Data.Migrations
{
    public partial class updateRouteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndPointId",
                table: "Route");

            migrationBuilder.AddColumn<Guid>(
                name: "RouteIdRoute",
                table: "Coordinate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinate_RouteIdRoute",
                table: "Coordinate",
                column: "RouteIdRoute");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinate_Route_RouteIdRoute",
                table: "Coordinate",
                column: "RouteIdRoute",
                principalTable: "Route",
                principalColumn: "IdRoute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinate_Route_RouteIdRoute",
                table: "Coordinate");

            migrationBuilder.DropIndex(
                name: "IX_Coordinate_RouteIdRoute",
                table: "Coordinate");

            migrationBuilder.DropColumn(
                name: "RouteIdRoute",
                table: "Coordinate");

            migrationBuilder.AddColumn<Guid>(
                name: "EndPointId",
                table: "Route",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
