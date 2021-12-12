using System.ComponentModel.DataAnnotations;

namespace WoodenGardenServer.Models.Database.GardenHouse
{
    public class GardenHouseModel
    {
        public int Id { get; set; }
        
        [Required] 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual List<GardenHouseImageModel>? GardenHouseImages { get; set; }
    }
}
