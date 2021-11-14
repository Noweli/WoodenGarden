using Microsoft.JSInterop;
using WoodenGardenApp.Client.Helpers.JsHelpers;
using WoodenGardenApp.Client.Helpers.ServiceHelpers;
using WoodenGardenApp.Client.Properties;
using WoodenGardenApp.Client.Services.IServices;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenApp.Shared.Helpers;

namespace WoodenGardenApp.Client.Services;

public class GardenHouseService : IGardenHouseService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public GardenHouseService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task AddGardenHouse(string? name, string? description)
    {
        if (name.IsNullOrWhiteSpace())
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseNameNotProvided);
        }

        var gardenHouseDTO = new GardenHouseDTO
        {
            Name = name,
            Description = description ?? string.Empty
        };

        try
        {
            var stringContent = gardenHouseDTO.GetStringContentAppJson();
            var result = await _httpClient.PostAsync($"{ApiConstants.Api_GardenHouseControllerUri}/add", stringContent);

            if (result.IsSuccessStatusCode)
            {
                await _jsRuntime.ToastrSuccess(Messages.Client_GardenHouseService_HouseAdded);
                return;
            }

            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseNotAdded);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseService_HouseNotAdded}\nException message: {e.Message}");
        }
    }

    public async Task DeleteGardenHouse(int? id)
    {
        if (id is null or < 0)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseIdNotProvided);
        }

        try
        {
            var requestUri = $"{ApiConstants.Api_GardenHouseControllerUri}/delete?id={id}";
            var result =
                await _httpClient.DeleteAsync(requestUri);

            if (result.IsSuccessStatusCode)
            {
                await _jsRuntime.ToastrSuccess(Messages.Client_GardenHouseService_HouseDeleted);
                return;
            }

            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseNotDeleted);

        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseService_HouseNotDeleted}\nException message: {e.Message}");
        }
    }

    public Task UpdateGardenHouse(int? id, string? name, string? description)
    {
        throw new NotImplementedException();
    }
}