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
            _ = await _dbContext.GardenHouseModels.AddAsync(gardenHouse);
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

    [HttpDelete]
    public async Task<IActionResult> DeleteGardenHouse(int? id)
    {
        if (id is null or < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseIdNotProvided);
        }

        var gardenHouseToRemove = await _dbContext.GardenHouseModels.FindAsync(id);

        if (gardenHouseToRemove is null)
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseToRemoveNotFound);
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
}