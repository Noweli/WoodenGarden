﻿using System.ComponentModel.DataAnnotations;

namespace WoodenGardenApp.Server.Models.Database.GardenHouse
{
    public class GardenHouseModel
    {
        public int Id { get; set; }
        
        [Required] 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<GardenHouseImageModel>? GardenHouseImages { get; set; }
        
    }
}