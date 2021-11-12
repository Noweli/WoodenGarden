using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using WoodenGardenApp.Server.Models;
using WoodenGardenApp.Server.Models.Database.GardenHouse;

namespace WoodenGardenApp.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    public DbSet<GardenHouseModel>? GardenHouseModels { get; set; }
    public DbSet<GardenHouseImageModel>? GardenHouseImageModels { get; set; }
}
