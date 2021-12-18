namespace WoodenGardenFront.Services.IServices;

public interface IGardenHouseImageService
{
    Task AddImage(int roomId, string imageBase64);
    Task DeleteImage(int imageId);
}