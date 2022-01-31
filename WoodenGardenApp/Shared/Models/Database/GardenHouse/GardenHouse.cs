using System.ComponentModel.DataAnnotations;

namespace WoodenGardenApp.Shared.Models.Database.GardenHouse
{
    public class GardenHouse
    {
        public int Id { get; set; }
        
        [Required] 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<GardenHouseImage>? GardenHouseImages { get; set; }
    }
}
