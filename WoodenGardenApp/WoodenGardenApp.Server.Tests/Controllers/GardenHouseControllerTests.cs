using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using WoodenGardenApp.Server.Controllers;
using WoodenGardenApp.Server.Data;
using WoodenGardenApp.Server.Properties;

namespace ServerUnitTests.Controllers;

[TestFixture]
public class GardenHouseControllerTests
{
    private GardenHouseController? _gardenHouseController;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder().Options;

        var appDbContextMock = new Mock<ApplicationDbContext>(options, null!);
        _gardenHouseController = new GardenHouseController(appDbContextMock.Object);
    }

    [TestCase(null, "Description")]
    [TestCase("", "Description")]
    public async Task AddGardenHouse_NameNotProvided_ReturnBadRequestObjectResult(string? name, string? description)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.AddGardenHouse(name!, description);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [TestCase(null, "Description")]
    [TestCase("", "Description")]
    public async Task AddGardenHouse_NameNotProvided_ReturnStatusCode400(string? name, string? description)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.AddGardenHouse(name!, description);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.StatusCode.Should().Be(400);
    }

    [TestCase(null, "Description")]
    [TestCase("", "Description")]
    public async Task AddGardenHouse_NameNotProvided_ReturnErrorMessage(string? name, string? description)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.AddGardenHouse(name!, description);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should().BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_NameIsEmpty);
    }

    [TestCase(null)]
    [TestCase(-1)]
    public async Task DeleteGardenHouse_IncorrectIdProvided_ReturnBadRequestObjectResult(int? id)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.DeleteGardenHouse(id);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [TestCase(null)]
    [TestCase(-1)]
    public async Task DeleteGardenHouse_IncorrectIdProvided_ReturnErrorMessage(int? id)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.DeleteGardenHouse(id);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should().BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_HouseIdNotProvided);
    }

    [TestCase(null)]
    [TestCase(-1)]
    public async Task UpdateGardenHouse_IncorrectIdProvided_ReturnBadRequestObjectResult(int? id)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.UpdateGardenHouse(id, string.Empty, string.Empty);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [TestCase(null)]
    [TestCase(-1)]
    public async Task UpdateGardenHouse_IncorrectIdProvided_ReturnErrorMessage(int? id)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.UpdateGardenHouse(id, string.Empty, string.Empty);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should().BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_IdToUpdateNotProvided);
    }
    
    [TestCase(null, null)]
    [TestCase(null, "")]
    [TestCase("", null)]
    [TestCase("", "")]
    public async Task UpdateGardenHouse_NameAndDescriptionNotProvided_ReturnBadRequestObjectResult(string? name, string? description)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.UpdateGardenHouse(1, name, description);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [TestCase(null, null)]
    [TestCase(null, "")]
    [TestCase("", null)]
    [TestCase("", "")]
    public async Task UpdateGardenHouse_NameAndDescriptionNotProvided_ReturnErrorMessage(string? name, string? description)
    {
        //Arrange
        //Act
        var result = await _gardenHouseController!.UpdateGardenHouse(1, name, description);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should().BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_NameAndDescriptionToUpdateEmpty);
    }
}