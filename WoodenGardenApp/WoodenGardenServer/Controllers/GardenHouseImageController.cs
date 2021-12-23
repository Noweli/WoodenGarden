using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        var imageToAdd = _mapper.Map<GardenHouseImageDTO, GardenHouseImageModel>(gardenHouseImageDTO);
        
        try
        {
            _ = await _dbContext.GardenHouseImageModels!.AddAsync(imageToAdd);
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