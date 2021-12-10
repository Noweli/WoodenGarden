using System.ComponentModel.DataAnnotations;

namespace WoodenGardenApp.Shared.DTOs;

public class GardenHouseDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "House name is required")]
    public string? Name { get; set; }
    public string? Description { get; set; }
}