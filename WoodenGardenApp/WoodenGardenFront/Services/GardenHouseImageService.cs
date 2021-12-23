using Microsoft.JSInterop;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenApp.Shared.Helpers;
using WoodenGardenFront.Helpers.JsHelpers;
using WoodenGardenFront.Helpers.ServiceHelpers;
using WoodenGardenFront.Properties;
using WoodenGardenFront.Services.IServices;

namespace WoodenGardenFront.Services;

public class GardenHouseImageService : IGardenHouseImageService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public GardenHouseImageService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task AddImage(int roomId, string imagePath)
    {
        if (roomId < 0)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseImageService_RoomIdNotProvided);
            return;
        }

        if (imagePath.IsNullOrWhiteSpace())
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseImageService_ImagePathNotProvided);
            return;
        }

        var gardenHouseImageDTO = new GardenHouseImageDTO
        {
            RoomId = roomId,
            ImagePath = imagePath
        };

        try
        {
            var stringContent = gardenHouseImageDTO.GetStringContentAppJson();
            var response = await _httpClient.PostAsync($"{ApiConstants.Api_GardenHouseImageControllerUri}/addimage", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseImageService_ResponseErrorImageNotAdded} Status code: {response.StatusCode}");
            }
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseServiceImage_ImageNotAddedException} Error message: {e.Message}");
        }
    }

    public async Task DeleteImage(int imageId)
    {
        if (imageId < 0)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_ImageIdsToDeleteNotProvided);
            return;
        }

        try
        {
            var response = await _httpClient.DeleteAsync(
                $"{ApiConstants.Api_GardenHouseImageControllerUri}\\deleteimages?ids={imageId}");

            if (!response.IsSuccessStatusCode)
            {
                await _jsRuntime.ToastrError(
                    $"{ErrorMessages.Client_GardenHouseServiceImage_ApiErrorImagesNotDeleted} Status code: {response.StatusCode}");
            }
        }
        catch (Exception)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseImageService_DeleteImagesException);
        }
    }
}