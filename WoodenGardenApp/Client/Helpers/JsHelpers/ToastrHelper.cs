using Microsoft.JSInterop;
using WoodenGardenApp.Client.Properties;
using WoodenGardenApp.Shared.Helpers;

namespace WoodenGardenApp.Client.Helpers.JsHelpers;

internal static class ToastrHelper
{
    internal static async ValueTask ToastrSuccess(this IJSRuntime runtime, string? message)
    {
        if (message.IsNullOrWhiteSpace())
        {
            await runtime.InvokeVoidAsync("ShowToastr", "success",
                ErrorMessages.Client_ToastrHelper_MessageNotProvided);
            return;
        }

        await runtime.InvokeVoidAsync("ShowToastr", "success", message);
    }
    
    internal static async ValueTask ToastrError(this IJSRuntime runtime, string? message)
    {
        if (message.IsNullOrWhiteSpace())
        {
            await runtime.InvokeVoidAsync("ShowToastr", "error",
                ErrorMessages.Client_ToastrHelper_MessageNotProvided);
            return;
        }

        await runtime.InvokeVoidAsync("ShowToastr", "error", message);
    }
}