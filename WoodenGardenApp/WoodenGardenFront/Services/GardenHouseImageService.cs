using Microsoft.JSInterop;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenFront.Helpers.JsHelpers;
using WoodenGardenFront.Helpers.ServiceHelpers;
using WoodenGardenFront.Properties;
using WoodenGardenFront.Services.IServices;

namespace WoodenGardenFront.Services;

public class GardenHouseImageService : IGardenHouseImageService
{
    private readonly HttpClient _httpClient;
    private readonly IFileUpload _fileUpload;
    private readonly IJSRuntime _jsRuntime;

    public GardenHouseImageService(HttpClient httpClient, IFileUpload fileUpload, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _fileUpload = fileUpload;
        _jsRuntime = jsRuntime;
    }

    public async Task AddImage(int roomId, List<string>? imageUrls)
    {
        if (roomId < 0)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseImageService_RoomIdNotProvided);
            return;
        }

        if (imageUrls is null || !imageUrls.Any())
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseImageService_ImageUrlsNotProvided);
            return;
        }

        var gardenHouseImageDTO = new GardenHouseImageDTO
        {
            RoomId = roomId,
            ImageUrls = imageUrls
        };

        try
        {
            var stringContent = gardenHouseImageDTO.GetStringContentAppJson();
            var response = await _httpClient.PostAsync($"{ApiConstants.Api_GardenHouseImageControllerUri}/addimages", stringContent);

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

    public async Task DeleteImage(List<int>? imageId)
    {
        if (imageId is null || !imageId.Any())
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