using AutoMapper;
using DataLayer.DataModel;
using System.Linq;
using DataTransfer;

namespace BusinessLayer.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            RegionMappingProfiles();
            VisitorMappingProfiles();
        }

        private void RegionMappingProfiles()
        {
            CreateMap<Region, RegionDto>()
                .ForMember(dto => dto.VisitorIds, opt => opt.MapFrom(region => region.Visitors.Select(x => x.Id).ToList()))
                .ReverseMap()
                    .ForMember(region => region.Visitors, opt => opt.Ignore());
        }
    
        private void VisitorMappingProfiles()
        {
            CreateMap<Visitor, VisitorDto>()
                .ForMember(dto => dto.RegionIds, opt => opt.MapFrom(visitor => visitor.Regions.Select(x => x.Id).ToList()))
                .ReverseMap()
                    .ForMember(visitor => visitor.Regions, opt => opt.Ignore());
            
        }
    }
}
