using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapMyPathCore.Data.Migrations
{
    public partial class editRouting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinate_Route_RouteIdRoute",
                table: "Coordinate");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_Coordinate_StartPointId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Route_StartPointId",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Coordinate_RouteIdRoute",
                table: "Coordinate");

            migrationBuilder.DropColumn(
                name: "StartPointId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "RouteIdRoute",
                table: "Coordinate");

            migrationBuilder.AddColumn<Guid>(
                name: "RouteId",
                table: "Coordinate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "StoppingOrder",
                table: "Coordinate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinate_RouteId",
                table: "Coordinate",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinate_Route_RouteId",
                table: "Coordinate",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "IdRoute",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinate_Route_RouteId",
                table: "Coordinate");

            migrationBuilder.DropIndex(
                name: "IX_Coordinate_RouteId",
                table: "Coordinate");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Coordinate");

            migrationBuilder.DropColumn(
                name: "StoppingOrder",
                table: "Coordinate");

            migrationBuilder.AddColumn<Guid>(
                name: "StartPointId",
                table: "Route",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RouteIdRoute",
                table: "Coordinate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Route_StartPointId",
                table: "Route",
                column: "StartPointId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Route_Coordinate_StartPointId",
                table: "Route",
                column: "StartPointId",
                principalTable: "Coordinate",
                principalColumn: "IdCoordinate",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
