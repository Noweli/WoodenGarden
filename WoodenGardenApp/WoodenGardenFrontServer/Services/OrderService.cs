using Microsoft.JSInterop;
using WoodenGardenApp.Shared.Models.Database;
using WoodenGardenFront.Helpers.JsHelpers;
using WoodenGardenFront.Helpers.ServiceHelpers;
using WoodenGardenFront.Properties;
using WoodenGardenFrontServer.Services.IServices;

namespace WoodenGardenFrontServer.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public OrderService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task AddOrder(Order order)
    {
        try
        {
            var stringContent = order.GetStringContentAppJson();
            var response = await _httpClient.PostAsync($"{ApiConstants.Api_OrderUri}/add", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                await _jsRuntime.ToastrError($"{ErrorMessages.Client_Order_AddFailed} Status: {response.StatusCode}");
            }

            await _jsRuntime.ToastrSuccess(Messages.Client_Order_OrderAdded);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_Order_AddFailed} Exception: {e.Message}");
        }
    }
}