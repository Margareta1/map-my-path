using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapMyPathCore.Data.Migrations
{
    public partial class inittables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coordinate",
                columns: table => new
                {
                    IdCoordinate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinate", x => x.IdCoordinate);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    IdRoute = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.IdRoute);
                    table.ForeignKey(
                        name: "FK_Route_Coordinate_StartPointId",
                        column: x => x.StartPointId,
                        principalTable: "Coordinate",
                        principalColumn: "IdCoordinate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Route_StartPointId",
                table: "Route",
                column: "StartPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Coordinate");
        }
    }
}
