namespace WoodenGardenFront.Services.IServices;

public interface IGardenHouseImageService
{
    Task AddImage(int roomId, List<string>? imageUrl);
    Task DeleteImage(List<int>? imageId);
}