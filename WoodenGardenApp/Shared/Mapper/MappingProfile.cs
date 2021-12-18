﻿using AutoMapper;
using WoodenGardenApp.Shared.DTOs;
using WoodenGardenApp.Shared.Models.Database.GardenHouse;

namespace WoodenGardenApp.Shared.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GardenHouseDTO, GardenHouseModel>().ReverseMap();
    }
}