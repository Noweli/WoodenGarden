using Microsoft.AspNetCore.Components.Forms;
using WoodenGardenApp.Client.Services.IServices;

namespace WoodenGardenApp.Client.Services;

public class FileUploadService : IFileUpload
{
    public Task<string> UploadFile(IBrowserFile file)
    {
        throw new NotImplementedException();
    }

    public bool DeleteFile(string fileName)
    {
        throw new NotImplementedException();
    }
}