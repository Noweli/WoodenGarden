using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WoodenGardenApp.Server.Data;
using WoodenGardenApp.Server.Helpers;
using WoodenGardenApp.Server.Models.Api;
using WoodenGardenApp.Server.Models.Database.GardenHouse;
using WoodenGardenApp.Server.Properties;

namespace WoodenGardenApp.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GardenHouseController
{
    private ApplicationDbContext _dbContext;

    public GardenHouseController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddGardenHouse(string name, string? description)
    {
        if (name.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_NameIsEmpty);
        }

        var gardenHouse = new GardenHouseModel
        {
            Name = name,
            Description = description ?? string.Empty
        };

        try
        {
            _ = await _dbContext.GardenHouseModels!.AddAsync(gardenHouse);
            _ = await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_GardenHouse_FailedToAddToDb,
                ExceptionMessage = e.Message
            });
        }

        return new OkResult();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteGardenHouse(int? id)
    {
        if (id is null or < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseIdNotProvided);
        }

        var gardenHouseToRemove = await _dbContext.GardenHouseModels!.FindAsync(id);

        if (gardenHouseToRemove is null)
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseWithIdNotFound);
        }

        try
        {
            _ = _dbContext.GardenHouseModels.Remove(gardenHouseToRemove);
            _ = await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_GardenHouse_FailedToRemoveFromDb,
                ExceptionMessage = e.Message
            });
        }

        return new OkResult();
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateGardenHouse(int? id, string? name, string? description)
    {
        if (id is null or < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_IdToUpdateNotProvided);
        }

        if (name.IsNullOrWhiteSpace() && description.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages
                .ApiError_GardenHouseValidation_NameAndDescriptionToUpdateEmpty);
        }

        var gardenHouseToUpdate = await _dbContext.GardenHouseModels!.FindAsync(id);

        if (gardenHouseToUpdate is null)
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseWithIdNotFound);
        }

        if (!name.IsNullOrWhiteSpace())
        {
            gardenHouseToUpdate.Name = name;
        }

        if (!description.IsNullOrWhiteSpace())
        {
            gardenHouseToUpdate.Description = description;
        }

        try
        {
            _ = _dbContext.GardenHouseModels.Update(gardenHouseToUpdate);
            _ = await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_GardenHouse_FailedToUpdateInDb,
                ExceptionMessage = e.Message
            });
        }

        return new OkResult();
    }

    [HttpPatch("addimages")]
    public async Task<IActionResult> AddImageToGardenHouse(int? id, List<string>? imageUrls)
    {
        if (id is null or < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_IdToAddImageNotProvided);
        }

        if (imageUrls is null || !imageUrls.Any())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImagesToAddNotProvided);
        }

        var gardenHouseToUpdate = await _dbContext.GardenHouseModels!.FindAsync(id);

        if (gardenHouseToUpdate is null)
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseWithIdNotFound);
        }

        var imageUlrModels = new List<GardenHouseImageModel>();
        
        imageUrls.ForEach(url => imageUlrModels.Add(new GardenHouseImageModel
        {
            ImageUrl = url,
            GardenHouseId = id.Value
        }));

        gardenHouseToUpdate.GardenHouseImages = imageUlrModels;

        try
        {
            _ = _dbContext.GardenHouseModels.Update(gardenHouseToUpdate);
            _ = await _dbContext.SaveChangesAsync();
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

    [HttpDelete("removeimages")]
    public async Task<IActionResult> RemoveImages(List<int>? ids)
    {
        if (ids is null || !ids.Any())
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided);
        }

        var imagesToDelete = _dbContext.GardenHouseImageModels!.Where(image => ids.Contains(image.Id)).ToList();

        if (!imagesToDelete.Any())
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImagesWithProvidedIdNotFound);
        }

        try
        {
            _dbContext.GardenHouseImageModels!.RemoveRange(imagesToDelete);
            _ = await _dbContext.SaveChangesAsync();
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
}