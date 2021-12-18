using System.ComponentModel.DataAnnotations.Schema;

namespace WoodenGardenApp.Shared.Models.Database.GardenHouse
{
    public class GardenHouseImageModel
    {
        public int Id { get; set; }
        public int GardenHouseId { get; set; }
        public string? ImageUrl { get; set; }
        
        [ForeignKey("GardenHouseId")] 
        public virtual GardenHouseModel? GardenHouse { get; set; }
    }
}
