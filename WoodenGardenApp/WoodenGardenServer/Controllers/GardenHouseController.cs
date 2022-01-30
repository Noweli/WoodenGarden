using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenApp.Shared.Helpers;
using WoodenGardenApp.Shared.Models.Api;
using WoodenGardenApp.Shared.Models.Database.GardenHouse;
using WoodenGardenServer.Data;
using WoodenGardenServer.Properties;

namespace WoodenGardenServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GardenHouseController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GardenHouseController(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddGardenHouse([FromBody] GardenHouseDTO gardenHouseDTO)
    {
        EntityEntry<GardenHouseModel> addedGardenHouse;
        
        if (gardenHouseDTO.Name.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_NameIsEmpty);
        }

        var gardenHouse = new GardenHouseModel
        {
            Name = gardenHouseDTO.Name,
            Description = gardenHouseDTO.Description ?? string.Empty
        };

        try
        {
            addedGardenHouse = await _dbContext.GardenHouseModels!.AddAsync(gardenHouse);
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

        return new CreatedResult("WoodenGarden", addedGardenHouse.Entity);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteGardenHouse(int id)
    {
        if (id < 0)
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
    public async Task<IActionResult> UpdateGardenHouse([FromBody]GardenHouseDTO gardenHouseDTO)
    {
        EntityEntry<GardenHouseModel> updatedGardenHouse;
        
        if (gardenHouseDTO.Id < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_IdToUpdateNotProvided);
        }

        if (gardenHouseDTO.Name.IsNullOrWhiteSpace() && gardenHouseDTO.Description.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages
                .ApiError_GardenHouseValidation_NameAndDescriptionToUpdateEmpty);
        }

        var gardenHouseToUpdate = await _dbContext.GardenHouseModels!.FindAsync(gardenHouseDTO.Id);

        if (gardenHouseToUpdate is null)
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouseValidation_HouseWithIdNotFound);
        }

        if (!gardenHouseDTO.Name.IsNullOrWhiteSpace())
        {
            gardenHouseToUpdate.Name = gardenHouseDTO.Name;
        }

        if (gardenHouseDTO.Description is not null)
        {
            gardenHouseToUpdate.Description = gardenHouseDTO.Description;
        }

        try
        {
            updatedGardenHouse = _dbContext.GardenHouseModels.Update(gardenHouseToUpdate);
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

        return new OkObjectResult(updatedGardenHouse.Entity);
    }

    [HttpGet("findAll")]
    public async Task<ActionResult<IEnumerable<GardenHouseDTO>>> GetAllWoodenHouses()
    {
        try
        {
            var gardenHousesList = await _dbContext.GardenHouseModels!
                .Include(model => model.GardenHouseImages).ToListAsync();

            var gardenHouseDTOs = gardenHousesList.Select(house => _mapper.Map<GardenHouseModel, GardenHouseDTO>(house)).ToList();

            return gardenHouseDTOs;

        }
        catch (Exception e)
        {
            return new UnprocessableEntityObjectResult(new ErrorModel
            {
                ErrorMessage = ErrorMessages.ApiError_GardenHouse_FailedToGetHouses,
                ExceptionMessage = e.Message
            });
        }
    }
}