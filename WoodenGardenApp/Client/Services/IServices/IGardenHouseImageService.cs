namespace WoodenGardenApp.Client.Services.IServices;

public interface IGardenHouseImageService
{
    Task AddImage(int? roomId, string imageUrl);
    Task DeleteImage(int? imageId);
}