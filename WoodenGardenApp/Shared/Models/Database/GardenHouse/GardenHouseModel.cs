using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoodenGardenApp.Shared.Models.Database.GardenHouse
{
    public class GardenHouseModel
    {
        public int Id { get; set; }
        
        [Required] 
        public string? Name { get; set; }
        public string? Description { get; set; }
        
        [NotMapped]
        public virtual ICollection? GardenHouseImages { get; set; }
    }
}
