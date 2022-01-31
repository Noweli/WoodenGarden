using System.ComponentModel.DataAnnotations;

namespace WoodenGardenApp.Shared.Models.Database;

public class Order
{
    public int Id { get; set; }
    [Required]
    public string? RequesterName { get; set; }
    [Required]
    [EmailAddress]
    public string? RequesterEmail { get; set; }
    [Phone]
    public string? RequesterPhoneNo { get; set; }
    [Required]
    public string? Description { get; set; }
}