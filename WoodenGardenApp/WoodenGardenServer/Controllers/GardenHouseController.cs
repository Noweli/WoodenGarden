﻿using AutoMapper;
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
    public async Task<IActionResult> UpdateGardenHouse(int id, string? name, string? description)
    {
        EntityEntry<GardenHouseModel> updatedGardenHouse;
        
        if (id < 0)
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

        if (description is not null)
        {
            gardenHouseToUpdate.Description = description;
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
                .Include(house => house.GardenHouseImages).ToListAsync();

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