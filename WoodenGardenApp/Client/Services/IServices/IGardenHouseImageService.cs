namespace WoodenGardenApp.Client.Services.IServices;

public interface IGardenHouseImageService
{
    Task AddImage(int? roomId, List<string>? imageUrl);
    Task DeleteImage(int? imageId);
}