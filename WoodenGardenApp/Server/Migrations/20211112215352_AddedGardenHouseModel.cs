using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodenGardenApp.Server.Migrations
{
    public partial class AddedGardenHouseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GardenHouseModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenHouseModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GardenHouseImageModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GardenHouseId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GardenHouseImageModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GardenHouseImageModel_GardenHouseModels_GardenHouseId",
                        column: x => x.GardenHouseId,
                        principalTable: "GardenHouseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GardenHouseImageModel_GardenHouseId",
                table: "GardenHouseImageModel",
                column: "GardenHouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GardenHouseImageModel");

            migrationBuilder.DropTable(
                name: "GardenHouseModels");
        }
    }
}
