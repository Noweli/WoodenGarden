using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodenGardenServer.Migrations
{
    public partial class ChangedImagesBase64ToImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageBase64",
                table: "GardenHouseImageModels",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "GardenHouseImageModels",
                newName: "ImageBase64");
        }
    }
}
