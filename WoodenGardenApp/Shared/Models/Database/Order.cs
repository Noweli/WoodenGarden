namespace WoodenGardenApp.Shared.Models.Database;

public class Order
{
    public int Id { get; set; }
    public string? RequesterName { get; set; }
    public string? RequesterEmail { get; set; }
    public string? RequesterPhoneNo { get; set; }
    public string? Description { get; set; }
}