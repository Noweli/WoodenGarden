using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using WoodenGardenApp.Client.Helpers.JsHelpers;
using WoodenGardenApp.Client.Properties;
using WoodenGardenApp.Client.Services.IServices;

namespace WoodenGardenApp.Client.Services;

public class FileUploadService : IFileUpload
{
    private const string ImagesDirectory = "GardenHouseImages";
    
    private readonly NavigationManager _navigationManager;
    private readonly IJSRuntime _jsRuntime;

    public FileUploadService(NavigationManager navigationManager, IJSRuntime jsRuntime)
    {
        _navigationManager = navigationManager;
        _jsRuntime = jsRuntime;
    }

    public async Task<string> UploadFile(IBrowserFile? file)
    {
        if (file is null)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_FileUpload_FileNameEmpty);
            return string.Empty;
        }

        var fileInfo = new FileInfo(file.Name);
        var fileName = Guid.NewGuid() + fileInfo.Extension;
        var path = Path.Combine(ImagesDirectory, fileName);

        if (!Directory.Exists(ImagesDirectory))
        {
            Directory.CreateDirectory(ImagesDirectory);
        }

        try
        {
            await using FileStream fs = new(path, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);
        }
        catch (Exception e)
        {
            await _jsRuntime.ToastrError($"{ErrorMessages.Client_FileUpload_FileNotAdded} Error message:\n{e.Message}");
        }

        return $"{_navigationManager.BaseUri}\\{ImagesDirectory}\\{fileName}";
    }

    public bool DeleteFile(string fileName)
    {
        throw new NotImplementedException();
    }
}