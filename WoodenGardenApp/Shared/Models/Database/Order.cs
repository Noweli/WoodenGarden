using System.ComponentModel.DataAnnotations;

namespace WoodenGardenApp.Shared.Models.Database;

public class Order
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    public string? RequesterName { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is required and needs to be valid email.")]
    public string? RequesterEmail { get; set; }
    [Phone(ErrorMessage = "Phone number, if entered, needs to be valid phone number.")]
    public string? RequesterPhoneNo { get; set; }
    [Required(ErrorMessage = "Order description is required.")]
    public string? Description { get; set; }
}