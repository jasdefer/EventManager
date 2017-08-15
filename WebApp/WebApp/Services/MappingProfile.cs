using AutoMapper;
using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model.Regions;
using WebApp.Model.Visitors;

namespace WebApp.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegionDto, AddRegionModel>().ReverseMap();
            CreateMap<VisitorDto, UpdateVisitorModel>().ReverseMap();
            CreateMap<DisplayRegionModel, RegionDto>().ReverseMap();
        }
    }
}
