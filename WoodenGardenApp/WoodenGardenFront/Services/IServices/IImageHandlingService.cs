using Microsoft.AspNetCore.Components.Forms;

namespace WoodenGardenFront.Services.IServices;

public interface IImageHandlingService
{
    Task<string> SaveImage(IBrowserFile image);
    bool DeleteImage(string imagePath);
}