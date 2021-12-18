using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using WoodenGardenApp.Shared.Helpers;
using WoodenGardenFront.Helpers.JsHelpers;
using WoodenGardenFront.Properties;
using WoodenGardenFront.Services.IServices;

namespace WoodenGardenFront.Services;

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

    public async Task<bool> DeleteFile(string fileName)
    {
        if (fileName.IsNullOrWhiteSpace())
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_FileUpload_DeleteFileNameEmpty);
            return false;
        }
        
        var path = $"{ImagesDirectory}\\{fileName}";

        if (!File.Exists(path))
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_FileUpload_FileToDeleteNotExists);
            return false;
        }

        try
        {
            File.Delete(path);
        }
        catch (Exception)
        {
            await _jsRuntime.ToastrError(ErrorMessages.Client_FileUpload_FileNotDeletedError);
        }

        return true;
    }
}