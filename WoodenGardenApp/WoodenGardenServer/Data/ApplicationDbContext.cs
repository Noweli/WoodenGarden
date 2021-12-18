using Microsoft.EntityFrameworkCore;
using WoodenGardenApp.Shared.Models.Database.GardenHouse;

namespace WoodenGardenServer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<GardenHouseModel>? GardenHouseModels { get; set; }
    public DbSet<GardenHouseImageModel>? GardenHouseImageModels { get; set; }
}
