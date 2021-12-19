using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodenGardenServer.Migrations
{
    public partial class GardenHouseImageContainsBase64Image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "GardenHouseImageModels",
                newName: "ImageBase64");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "GardenHouseImageModels",
                newName: "ImageUrl");
        }
    }
}
