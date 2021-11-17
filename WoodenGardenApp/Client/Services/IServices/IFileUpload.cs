using Microsoft.AspNetCore.Components.Forms;

namespace WoodenGardenApp.Client.Services.IServices;

public interface IFileUpload
{
    Task<string> UploadFile(IBrowserFile? file);
    Task<bool> DeleteFile(string fileName);
}