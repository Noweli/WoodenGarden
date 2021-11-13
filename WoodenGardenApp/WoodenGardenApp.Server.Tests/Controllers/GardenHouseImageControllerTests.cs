using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WoodenGardenApp.Server.Controllers;
using WoodenGardenApp.Server.Data;

namespace ServerUnitTests.Controllers;

[TestFixture]
public class GardenHouseImageControllerTests
{
    private GardenHouseController? _gardenHouseController;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder().Options;

        var appDbContextMock = new Mock<ApplicationDbContext>(options, null!);
        _gardenHouseController = new GardenHouseController(appDbContextMock.Object);
    }
    
    
}