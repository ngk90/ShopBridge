using AutoMapper;
using ShopBridge.DTO;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Inventory, InventoryDto>().ReverseMap();
        }
    }
}
