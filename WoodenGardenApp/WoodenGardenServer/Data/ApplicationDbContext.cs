using Microsoft.EntityFrameworkCore;
using WoodenGardenApp.Server.Models.Database.GardenHouse;

namespace WoodenGardenApp.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions options) : base(options)
    { }

    public DbSet<GardenHouseModel>? GardenHouseModels { get; set; }
    public DbSet<GardenHouseImageModel>? GardenHouseImageModels { get; set; }
}
