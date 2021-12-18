using WoodenGardenApp.Shared.DTOs;

namespace WoodenGardenFront.Services.IServices;

public interface IGardenHouseService
{
    Task AddGardenHouse(string? name, string? description);
    Task DeleteGardenHouse(int id);
    Task UpdateGardenHouse(int id, string? name, string? description);
    Task<IEnumerable<GardenHouseDTO>?> GetGardenHouses();
}