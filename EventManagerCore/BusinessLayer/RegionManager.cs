using AutoMapper;
using BusinessLayer.BusinessExceptions;
using DataLayer.DataModel;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using DataTransfer;

namespace BusinessLayer
{
    public class RegionManager
    {
        private readonly IVistorRepository _visitorRepository;
        private readonly IMapper _mapper;
        private readonly IRegionRepository _regionRepository;

        public RegionManager(IVistorRepository visitorRepository,
            IMapper mapper,
            IRegionRepository regionRepository)
        {
            _visitorRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
        }

        public int Add(RegionDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Region region = _mapper.Map<Region>(dto);
            int[] vistorIds = dto.VisitorIds?.ToArray() ?? new int[0];

            //Check if only existing visitors are visiting the new region
            foreach (var visitorId in vistorIds)
            {
                if(_visitorRepository.Get(visitorId) == null) throw new BusinessException("Cannot add a region with unkown visitors.");
            }

            //Add the region
            region.Visitors = null;
            _regionRepository.Add(region);

            //Add all existing visitors to this reigon
            foreach (var visitorId in vistorIds)
            {
                _regionRepository.AddVisitor(region.Id, visitorId);
            }

            return region.Id;
        }

        public void Delete(int regionId)
        {
            //Load the region
            if(_regionRepository.Get(regionId)==null) throw new KeyNotFoundException("Cannot find region with the id " + regionId);

            //Delete the visits
            _regionRepository.RemoveAllVisitors(regionId);

            //Delete the region
            _regionRepository.Delete(regionId);
        }

        public void Update(RegionDto dto)
        {
            //Load and validate the region
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Region region = _regionRepository.Get(dto.Id);
            if (region == null) throw new KeyNotFoundException("Cannot find the region with the id " + dto.Id);
            _mapper.Map(dto, region);

            //Check if only existing visitors are visiting the region
            int[] visitorIds = dto.VisitorIds?.ToArray() ?? new int[0];
            foreach (var visitorId in visitorIds)
            {
                if (_visitorRepository.Get(visitorId) == null) throw new BusinessException("Cannot add a region with unkown visitors.");
            }

            //Update the region
            _regionRepository.Update(region);

            //Update the relationships
            int[] current = _regionRepository.GetAllVisitors(region.Id)?.ToArray() ?? new int[0];

            //Delete
            foreach (var visitorId in current.Except(visitorIds).ToArray())
            {
                _regionRepository.RemoveVisitor(region.Id, visitorId);
            }

            //Add
            foreach (var visitorId in visitorIds.Except(current).ToArray())
            {
                _regionRepository.AddVisitor(region.Id, visitorId);
            }

        }

        /// <summary>
        /// Get the region with the given id.
        /// </summary>
        public RegionDto Get(int regionId)
        {
            Region region = _regionRepository.Get(regionId);
            if (region == null) return null;

            var visitorIds = _regionRepository.GetAllVisitors(regionId);
            RegionDto dto = _mapper.Map<RegionDto>(region);
            dto.VisitorIds = visitorIds;
            return dto;
        }

        public IEnumerable<RegionDto> Get()
        {
            IEnumerable<Region> regions = _regionRepository.GetAll();
            List<RegionDto> dtos =  _mapper.Map<List<RegionDto>>(regions);
            foreach (var dto in dtos)
            {
                dto.VisitorIds = _regionRepository.GetAllVisitors(dto.Id);
            }

            return dtos;
        }
    }
}
