using Microsoft.EntityFrameworkCore;
using WoodenGardenApp.Shared.Models.Database;
using WoodenGardenApp.Shared.Models.Database.GardenHouse;

namespace WoodenGardenServer.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<GardenHouse>? GardenHouses { get; set; }
    public DbSet<GardenHouseImage>? GardenHouseImages { get; set; }
    public DbSet<Order>? Orders { get; set; }
}
