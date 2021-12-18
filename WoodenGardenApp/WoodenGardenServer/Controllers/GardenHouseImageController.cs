using Microsoft.AspNetCore.Mvc;
using WoodenGardenApp.Shared.Models.Api;
using WoodenGardenApp.Shared.Models.Database.GardenHouse;
using WoodenGardenServer.Data;
using WoodenGardenServer.Properties;

namespace WoodenGardenServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GardenHouseImageController
{
    private readonly ApplicationDbContext _dbContext;

    public GardenHouseImageController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("addimages")]
    public async Task<IActionResult> AddImage(int roomId, List<string>? imageUrls)
    {
        if (roomId < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_IdToAddImageNotProvided);
        }

        if (imageUrls is null || !imageUrls.Any())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImagesToAddNotProvided);
        }

        var imagesToAdd = new List<GardenHouseImageModel>();
        
        imageUrls.ForEach(imageUrl => imagesToAdd.Add(new GardenHouseImageModel
        {
            GardenHouseId = roomId,
            ImageUrl = imageUrl
        }));

        try
        {
            await _dbContext.GardenHouseImageModels!.AddRangeAsync(imagesToAdd);
            _ = _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_GardenHouse_AddImagesFailed,
                ExceptionMessage = e.Message
            });
        }

        return new OkResult();
    }

    [HttpDelete("deleteimages")]
    public async Task<IActionResult> DeleteImages(List<int>? ids)
    {
        if (ids is null || !ids.Any())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided);
        }

        var imagesToBeDeleted = _dbContext.GardenHouseImageModels!.Where(image => ids.Contains(image.Id)).ToList();

        if (!imagesToBeDeleted.Any())
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouse_ImagesNotFound);
        }

        try
        {
            _dbContext.GardenHouseImageModels!.RemoveRange(imagesToBeDeleted);
            _ = await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_GardenHouse_FailedToDeleteImagesFromDb,
                ExceptionMessage = e.Message
            });
        }

        return new OkResult();
    }
}