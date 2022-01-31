using System.ComponentModel.DataAnnotations;
using WoodenGardenApp.Shared.Models.Database.GardenHouse;

namespace WoodenGardenApp.Shared.DTOs;

public class GardenHouseDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "House name is required")]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<GardenHouseImage>? GardenHouseImages { get; set; }
}