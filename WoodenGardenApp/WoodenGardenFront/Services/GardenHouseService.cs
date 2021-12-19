using Microsoft.JSInterop;
using Newtonsoft.Json;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenApp.Shared.Helpers;
using WoodenGardenFront.Helpers.JsHelpers;
using WoodenGardenFront.Helpers.ServiceHelpers;
using WoodenGardenFront.Properties;
using WoodenGardenFront.Services.IServices;

namespace WoodenGardenFront.Services;

public class GardenHouseService : IGardenHouseService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public GardenHouseService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<int> AddGardenHouse(string? name, string? description)
    {
        if (name.IsNullOrWhiteSpace())
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseNameNotProvided);
            return 0;
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
                var resultContent = result.Content.ToString() ?? string.Empty;
                var resultHouse = JsonConvert.DeserializeObject<GardenHouseDTO>(resultContent);
                
                await _jsRuntime.ToastrSuccess(Messages.Client_GardenHouseService_HouseAdded);
                return resultHouse?.Id ?? 0;
            }

            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseNotAdded);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseService_HouseNotAdded}\nException message: {e.Message}");
        }

        return 0;
    }

    public async Task DeleteGardenHouse(int id)
    {
        if (id < 0)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseIdNotProvided);
            return;
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

    public async Task UpdateGardenHouse(int id, string? name, string? description)
    {
        if (id < 0)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseIdNotProvided);
            return;
        }

        if (name.IsNullOrWhiteSpace() && description.IsNullOrWhiteSpace())
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_NameAndDescriptionToUpdateNotProvided);
            return;
        }

        var gardenHouseDTO = new GardenHouseDTO
        {
            Id = id,
            Name = name,
            Description = description
        };

        try
        {
            var requestUri = $"{ApiConstants.Api_GardenHouseControllerUri}/update";
            var stringContent = gardenHouseDTO.GetStringContentAppJson();
            var result = await _httpClient.PatchAsync(requestUri, stringContent);

            if (result.IsSuccessStatusCode)
            {
                await _jsRuntime.ToastrSuccess(Messages.Client_GardenHouseService_HouseUpdated);
                return;
            }

            await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_HouseNotUpdated);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseService_HouseNotUpdated}\nException message: {e.Message}");
        }
    }
    
    public async Task<IEnumerable<GardenHouseDTO>?> GetGardenHouses()
    {
        try
        {
            var requestUri = $"{ApiConstants.Api_GardenHouseControllerUri}/findAll";
            var result =
                await _httpClient.GetAsync(requestUri);

            if (result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var gardenHouses = JsonConvert.DeserializeObject<IEnumerable<GardenHouseDTO>>(resultContent);

                if (gardenHouses is not null)
                {
                    return gardenHouses;
                }
                
                await _jsRuntime.ToastrError(ErrorMessages.Client_GardenHouseService_CouldNotGatherHouses); 
                
                return null;
            }

            await _jsRuntime.ToastrError(
                $"{ErrorMessages.Client_GardenHouseService_CouldNotGatherHouses}\nStatus code: {result.StatusCode}");
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_GardenHouseService_GetHousesError}\nException message: {e.Message}");
        }

        return null;
    }
}