using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using WoodenGardenFront.Helpers.JsHelpers;
using WoodenGardenFront.Properties;
using WoodenGardenFront.Services.IServices;

namespace WoodenGardenFront.Services;

public class ImageHandlingService : IImageHandlingService
{
    private readonly IJSRuntime _jsRuntime;

    public ImageHandlingService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> SaveImage(IBrowserFile image)
    {
        var fileInfo = new FileInfo(image.Name);
        var fileNameGuid = Guid.NewGuid() + fileInfo.Extension;
        var imagesDirectory = Path.Combine("images", "home");
        var filePath = Path.Combine(imagesDirectory, fileNameGuid);

        if (!Directory.Exists(imagesDirectory))
        {
            Directory.CreateDirectory(imagesDirectory);
        }

        try
        {
            await using var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            
            var memoryStream = new MemoryStream();
            await image.OpenReadStream().CopyToAsync(memoryStream);

            memoryStream.WriteTo(fs);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_FileUpload_FileNotAdded}\nError message: {e.Message}");
            
            return string.Empty;
        }

        return filePath;
    }

    public async Task<bool> DeleteImage(string imagePath)
    {
        try
        {
            if (!File.Exists(imagePath))
            {
                return false;
            }

            File.Delete(imagePath);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError(
                $"{ErrorMessages.Client_FileUpload_FileNotDeletedError}\nError message: {e.Message}");

            return false;
        }

        return true;
    }
}