using AutoMapper;
using BusinessLayer.BusinessExceptions;
using DataLayer.DataModel;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

namespace BusinessLayer
{
    public class RegionManager
    {
        private readonly IVistorRepository VisitorRepository;
        private readonly IMapper Mapper;
        private readonly IRegionRepository RegionRepository;

        public RegionManager(IVistorRepository visitorRepository,
            IMapper mapper,
            IRegionRepository regionRepository)
        {
            VisitorRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            RegionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
        }

        public int Add(RegionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Region region = Mapper.Map<Region>(dto);
            dto.VisitorIds = dto.VisitorIds ?? new List<int>();

            //Check if only existing visitors are visiting the new region
            foreach (var visitorId in dto.VisitorIds)
            {
                if(VisitorRepository.Get(visitorId) == null) throw new BusinessException("Cannot add a region with unkown visitors.");
            }

            //Add the region
            region.Visitors = null;
            RegionRepository.Add(region);

            //Add all existing visitors to this reigon
            foreach (var visitorId in dto.VisitorIds)
            {
                RegionRepository.AddVisitor(region.Id, visitorId);
            }

            return region.Id;
        }

        public void Delete(int regionId)
        {
            //Load the region
            Region region = RegionRepository.Get(regionId) ?? throw new KeyNotFoundException("Cannot find region with the id " + regionId);

            //Delete the visits
            RegionRepository.RemoveAllVisitors(regionId);

            //Delete the region
            RegionRepository.Delete(regionId);
        }

        public void Update(RegionDto dto)
        {
            //Load and validate the region
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Region region = RegionRepository.Get(dto.Id);
            if (region == null) throw new KeyNotFoundException("Cannot find the region with the id " + dto.Id);
            Mapper.Map(dto, region);

            //Check if only existing visitors are visiting the region
            dto.VisitorIds = dto.VisitorIds ?? new List<int>();
            foreach (var visitorId in dto.VisitorIds)
            {
                if (VisitorRepository.Get(visitorId) == null) throw new BusinessException("Cannot add a region with unkown visitors.");
            }

            //Update the region
            RegionRepository.Update(region);

            //Update the relationships
            int[] current = RegionRepository.GetAllVisitors(region.Id)?.ToArray() ?? new int[0];

            //Delete
            foreach (var visitorId in current.Except(dto.VisitorIds).ToArray())
            {
                RegionRepository.RemoveVisitor(region.Id, visitorId);
            }

            //Add
            foreach (var visitorId in dto.VisitorIds.Except(current).ToArray())
            {
                RegionRepository.AddVisitor(region.Id, visitorId);
            }

        }

        /// <summary>
        /// Get the region with the given id.
        /// </summary>
        public RegionDto Get(int regionId)
        {
            Region region = RegionRepository.Get(regionId);
            if (region == null) return null;

            var visitorIds = RegionRepository.GetAllVisitors(regionId);
            RegionDto dto = Mapper.Map<RegionDto>(region);
            dto.VisitorIds = visitorIds;
            return dto;
        }

        public IEnumerable<RegionDto> Get()
        {
            IEnumerable<Region> regions = RegionRepository.GetAll();
            List<RegionDto> dtos =  Mapper.Map<List<RegionDto>>(regions);
            foreach (var dto in dtos)
            {
                dto.VisitorIds = RegionRepository.GetAllVisitors(dto.Id);
            }

            return dtos;
        }
    }
}
