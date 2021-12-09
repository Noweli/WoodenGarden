using System.Collections.Generic;
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
public class GardenHouseImageControllerTests
{
    private GardenHouseImageController? _gardenHouseImageController;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder().Options;

        var appDbContextMock = new Mock<ApplicationDbContext>(options, null!);
        _gardenHouseImageController = new GardenHouseImageController(appDbContextMock.Object);
    }

    [TestCase(null)]
    [TestCase(-1)]
    public async Task AddImage_RoomIdIncorrect_ReturnBadRequestObjectResult(int roomId)
    {
        //Arrange
        //Act
        var result = await _gardenHouseImageController!.AddImage(roomId, null);
        
        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [TestCase(null)]
    [TestCase(-1)]
    public async Task AddImage_RoomIdIncorrect_ReturnErrorMessage(int roomId)
    {
        //Arrange
        //Act
        var result = await _gardenHouseImageController!.AddImage(roomId, null);
        var badRequestObjectResult = result as BadRequestObjectResult;
        
        //Assert
        badRequestObjectResult?.Value.Should()
            .BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_IdToAddImageNotProvided);
    }
    
    [Test]
    public async Task AddImage_ImageUrlsIsNull_ReturnBadRequestObjectResult()
    {
        //Arrange
        List<string>? imageUrls = null;
        
        //Act
        var result = await _gardenHouseImageController!.AddImage(1, imageUrls);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Test]
    public async Task AddImage_ImageUrlsIsNull_ReturnErrorMessage()
    {
        //Arrange
        List<string>? imageUrls = null;
        
        //Act
        var result = await _gardenHouseImageController!.AddImage(1, imageUrls);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should()
            .BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_ImagesToAddNotProvided);
    }
    
    [Test]
    public async Task AddImage_ImageUrlsIsEmpty_ReturnBadRequestObjectResult()
    {
        //Arrange
        List<string> imageUrls = new();
        
        //Act
        var result = await _gardenHouseImageController!.AddImage(1, imageUrls);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Test]
    public async Task AddImage_ImageUrlsIsEmpty_ReturnErrorMessage()
    {
        //Arrange
        List<string> imageUrls = new();
        
        //Act
        var result = await _gardenHouseImageController!.AddImage(1, imageUrls);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should().BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_ImagesToAddNotProvided);
    }
    
    [Test]
    public async Task DeleteImages_ImageIdsIsNull_ReturnBadRequestObjectResult()
    {
        //Arrange
        List<int>? imageIds = null;
        
        //Act
        var result = await _gardenHouseImageController!.DeleteImages(imageIds);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Test]
    public async Task DeleteImages_ImageIdsIsNull_ReturnErrorMessage()
    {
        //Arrange
        List<int>? imageIds = null;
        
        //Act
        var result = await _gardenHouseImageController!.DeleteImages(imageIds);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should()
            .BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided);
    }
    
    [Test]
    public async Task DeleteImages_ImageIdsIsEmpty_ReturnBadRequestObjectResult()
    {
        //Arrange
        List<int> imageIds = new();
        
        //Act
        var result = await _gardenHouseImageController!.DeleteImages(imageIds);

        //Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Test]
    public async Task DeleteImages_ImageIdsIsEmpty_ReturnErrorMessage()
    {
        //Arrange
        List<int> imageIds = new();
        
        //Act
        var result = await _gardenHouseImageController!.DeleteImages(imageIds);
        var badRequestObjectResult = result as BadRequestObjectResult;

        //Assert
        badRequestObjectResult?.Value.Should()
            .BeSameAs(ErrorMessages.ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided);
    }
}