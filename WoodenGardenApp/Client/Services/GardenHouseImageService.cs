using Microsoft.JSInterop;
using WoodenGardenApp.Client.Helpers.JsHelpers;
using WoodenGardenApp.Client.Helpers.ServiceHelpers;
using WoodenGardenApp.Client.Properties;
using WoodenGardenApp.Client.Services.IServices;
using WoodenGardenApp.Shared.DTOs;

namespace WoodenGardenApp.Client.Services;

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

    public async Task AddImage(int? roomId, List<string>? imageUrls)
    {
        if (roomId is null or < 0)
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
            RoomId = roomId.Value,
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

    public Task DeleteImage(int? imageId)
    {
        throw new NotImplementedException();
    }
}