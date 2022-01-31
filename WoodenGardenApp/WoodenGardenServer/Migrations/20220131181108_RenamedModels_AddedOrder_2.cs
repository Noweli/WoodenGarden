using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WoodenGardenServer.Migrations
{
    public partial class RenamedModels_AddedOrder_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GardenHouseImageModels_GardenHouseModels_GardenHouseId",
                table: "GardenHouseImageModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GardenHouseModels",
                table: "GardenHouseModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GardenHouseImageModels",
                table: "GardenHouseImageModels");

            migrationBuilder.RenameTable(
                name: "GardenHouseModels",
                newName: "GardenHouses");

            migrationBuilder.RenameTable(
                name: "GardenHouseImageModels",
                newName: "GardenHouseImages");

            migrationBuilder.RenameIndex(
                name: "IX_GardenHouseImageModels_GardenHouseId",
                table: "GardenHouseImages",
                newName: "IX_GardenHouseImages_GardenHouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GardenHouses",
                table: "GardenHouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GardenHouseImages",
                table: "GardenHouseImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequesterEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequesterPhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GardenHouseImages_GardenHouses_GardenHouseId",
                table: "GardenHouseImages",
                column: "GardenHouseId",
                principalTable: "GardenHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GardenHouseImages_GardenHouses_GardenHouseId",
                table: "GardenHouseImages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GardenHouses",
                table: "GardenHouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GardenHouseImages",
                table: "GardenHouseImages");

            migrationBuilder.RenameTable(
                name: "GardenHouses",
                newName: "GardenHouseModels");

            migrationBuilder.RenameTable(
                name: "GardenHouseImages",
                newName: "GardenHouseImageModels");

            migrationBuilder.RenameIndex(
                name: "IX_GardenHouseImages_GardenHouseId",
                table: "GardenHouseImageModels",
                newName: "IX_GardenHouseImageModels_GardenHouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GardenHouseModels",
                table: "GardenHouseModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GardenHouseImageModels",
                table: "GardenHouseImageModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GardenHouseImageModels_GardenHouseModels_GardenHouseId",
                table: "GardenHouseImageModels",
                column: "GardenHouseId",
                principalTable: "GardenHouseModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
