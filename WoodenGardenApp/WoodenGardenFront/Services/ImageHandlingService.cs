using Microsoft.AspNetCore.Components.Forms;
using WoodenGardenFront.Services.IServices;

namespace WoodenGardenFront.Services;

public class ImageHandlingService : IImageHandlingService
{
    public Task<string> SaveImage(IBrowserFile image)
    {
        throw new NotImplementedException();
    }

    public bool DeleteImage(string imagePath)
    {
        throw new NotImplementedException();
    }
}