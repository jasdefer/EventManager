using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using DataTransfer;
using AutoMapper;
using DataLayer.DataModel;
using BusinessLayer.BusinessExceptions;

namespace BusinessLayer
{
    public class VisitorManager
    {
        private readonly IVistorRepository _visitorRepository;
        private readonly IMapper _mapper;
        private readonly IRegionRepository _regionRepository;

        public VisitorManager(IVistorRepository visitorRepository, 
            IMapper mapper,
            IRegionRepository regionRepository)
        {
            _visitorRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
        }

        public int Add(VisitorDto dto)
        {
            //Validate the input
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Visitor visitor = _mapper.Map<Visitor>(dto);
            int[] regionIds = dto.RegionIds?.ToArray() ?? new int[0];
            foreach (var regionId in regionIds)
            {
                if (_regionRepository.Get(regionId) == null) throw new BusinessException("Cannot add a visitor with unkown regions.");
            }

            //Add the visitor without the regions
            visitor.Regions = null;
            _visitorRepository.Add(visitor);

            //Add all regions to the visitor
            foreach (var regionId in regionIds)
            {
                _regionRepository.AddVisitor(regionId, visitor.Id);
            }

            return visitor.Id;
        }

        public void Update(VisitorDto dto)
        {
            //Validate the visitor
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Visitor visitor = _visitorRepository.Get(dto.Id);
            if (visitor == null) throw new BusinessException("Cannot find an existing visitor with the id " + dto.Id);
            _mapper.Map(dto, visitor);

            //Check if only existing regions exist within the visitor
            foreach (var regionId in dto.RegionIds)
            {
                if (_regionRepository.Get(regionId) == null) throw new BusinessException("Cannot add a visitor with unkown regions.");
            }

            //Update the visitor
            _visitorRepository.Update(visitor);

            //Update his relationships
            List<int> current = _visitorRepository.GetAllVisiting(visitor.Id).ToList();
            
            //Delete
            foreach (var regionId in current.Except(dto.RegionIds).ToArray())
            {
                _regionRepository.RemoveVisitor(regionId, visitor.Id);
            }

            //Add
            foreach (var regionId in dto.RegionIds.Except(current).ToArray())
            {
                _regionRepository.AddVisitor(regionId, visitor.Id);
            }
        }

        public void Delete(int visitorId)
        {
            //Load the visitor
            Visitor visitor = _visitorRepository.Get(visitorId) ?? throw new BusinessException("Cannot find visitor with the id " + visitorId);

            //Load all visiting regions
            int[] regionIds = _visitorRepository.GetAllVisiting(visitorId).ToArray();

            //Delete his visits
            foreach (var regionId in regionIds)
            {
                _regionRepository.RemoveVisitor(regionId, visitor.Id);
            }
            
            //Delete the visitor
            _visitorRepository.Delete(visitorId);
        }

        public VisitorDto Get(int visitorId)
        {
            Visitor visitor = _visitorRepository.Get(visitorId);
            if (visitor == null) return null; 

            VisitorDto dto = _mapper.Map<VisitorDto>(visitor);
            dto.RegionIds = _visitorRepository.GetAllVisiting(visitorId);
            return dto;
        }

        public IEnumerable<VisitorDto> Get()
        {
            IEnumerable<Visitor> visitors = _visitorRepository.GetAll();
            List<VisitorDto> dtos = _mapper.Map<List<VisitorDto>>(visitors);

            foreach (var dto in dtos)
            {
                dto.RegionIds = _visitorRepository.GetAllVisiting(dto.Id);
            }

            return dtos;
        }
    }
}
