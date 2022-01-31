using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenApp.Shared.Helpers;
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
    private readonly IMapper _mapper;

    public GardenHouseImageController(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpPost("addimage")]
    public async Task<IActionResult> AddImage([FromBody] GardenHouseImageDTO gardenHouseImageDTO)
    {
        if (gardenHouseImageDTO.RoomId < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_IdToAddImageNotProvided);
        }

        if (gardenHouseImageDTO.ImagePath.IsNullOrWhiteSpace())
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImagePathNotProvided);
        }

        var imageToAdd = _mapper.Map<GardenHouseImageDTO, GardenHouseImage>(gardenHouseImageDTO);
        
        try
        {
            _ = await _dbContext.GardenHouseImages!.AddAsync(imageToAdd);
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

    [HttpDelete("deleteimage")]
    public async Task<IActionResult> DeleteImages(int id)
    {
        if (id < 0)
        {
            return new BadRequestObjectResult(ErrorMessages.ApiError_GardenHouseValidation_ImageIdsToDeleteNotProvided);
        }

        var imageToBeDeleted =
            await _dbContext.GardenHouseImages!.FirstOrDefaultAsync(image => image.Id.Equals(id));

        if (imageToBeDeleted is null)
        {
            return new NotFoundObjectResult(ErrorMessages.ApiError_GardenHouse_ImagesNotFound);
        }

        try
        {
            _dbContext.GardenHouseImages!.Remove(imageToBeDeleted);
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