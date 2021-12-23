using Microsoft.AspNetCore.Components.Forms;

namespace WoodenGardenFrontServer.Services.IServices;

public interface IImageHandlingService
{
    Task<string> SaveImage(IBrowserFile image);
    Task<bool> DeleteImage(string imagePath);
}