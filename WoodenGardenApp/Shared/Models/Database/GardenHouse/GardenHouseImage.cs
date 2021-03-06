using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WoodenGardenApp.Shared.Models.Database.GardenHouse
{
    public class GardenHouseImage
    {
        public int Id { get; set; }
        public int GardenHouseId { get; set; }
        public string? ImagePath { get; set; }

        [JsonIgnore]
        [ForeignKey("GardenHouseId")]
        public virtual GardenHouse? GardenHouse { get; set; }
    }
}
