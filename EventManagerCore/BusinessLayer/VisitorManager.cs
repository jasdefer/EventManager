using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.Dto;
using AutoMapper;
using DataLayer.DataModel;
using ValidationRules.PropertyValidation;
using BusinessLayer.BusinessExceptions;

namespace BusinessLayer
{
    public class VisitorManager
    {
        private readonly IVistorRepository VisitorRepository;
        private readonly IMapper Mapper;
        private readonly IRegionRepository RegionRepository;

        public VisitorManager(IVistorRepository visitorRepository, 
            IMapper mapper,
            IRegionRepository regionRepository)
        {
            VisitorRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            RegionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
        }

        public int Add(VisitorDto dto)
        {
            //Validate the input
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Visitor visitor = Mapper.Map<Visitor>(dto);
            visitor.IsValid();
            dto.RegionIds = dto.RegionIds ?? new List<int>();
            foreach (var regionId in dto.RegionIds)
            {
                if (RegionRepository.Get(regionId) == null) throw new BusinessException("Cannot add a visitor with unkown regions.");
            }

            //Add the visitor without the regions
            visitor.Regions = null;
            VisitorRepository.Add(visitor);

            //Add all regions to the visitor
            foreach (var regionId in dto.RegionIds)
            {
                RegionRepository.AddVisitor(regionId, visitor.Id);
            }

            return visitor.Id;
        }

        public void Update(VisitorDto dto)
        {
            //Validate the visitor
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            Visitor visitor = VisitorRepository.Get(dto.Id);
            if (visitor == null) throw new BusinessException("Cannot find an existing visitor with the id " + dto.Id);
            Mapper.Map(dto, visitor);
            visitor.IsValid();

            //Check if only existing regions exist within the visitor
            foreach (var regionId in dto.RegionIds)
            {
                if (RegionRepository.Get(regionId) == null) throw new BusinessException("Cannot add a visitor with unkown regions.");
            }

            //Update the visitor
            VisitorRepository.Update(visitor);

            //Update his relationships
            List<int> current = VisitorRepository.GetAllVisiting(visitor.Id).ToList();
            
            //Delete
            foreach (var regionId in current.Except(dto.RegionIds).ToArray())
            {
                RegionRepository.RemoveVisitor(regionId, visitor.Id);
            }

            //Add
            foreach (var regionId in dto.RegionIds.Except(current).ToArray())
            {
                RegionRepository.AddVisitor(regionId, visitor.Id);
            }
        }

        public void Delete(int visitorId)
        {
            //Load the visitor
            Visitor visitor = VisitorRepository.Get(visitorId) ?? throw new BusinessException("Cannot find visitor with the id " + visitorId);

            //Load all visiting regions
            int[] regionIds = VisitorRepository.GetAllVisiting(visitorId).ToArray();

            //Delete his visits
            foreach (var regionId in regionIds)
            {
                RegionRepository.RemoveVisitor(regionId, visitor.Id);
            }
            
            //Delete the visitor
            VisitorRepository.Delete(visitorId);
        }

        public VisitorDto Get(int visitorId)
        {
            Visitor visitor = VisitorRepository.Get(visitorId);
            if (visitor == null) return null; 

            VisitorDto dto = Mapper.Map<VisitorDto>(visitor);
            dto.RegionIds = VisitorRepository.GetAllVisiting(visitorId);
            return dto;
        }

        public IEnumerable<VisitorDto> Get()
        {
            IEnumerable<Visitor> visitors = VisitorRepository.GetAll();
            List<VisitorDto> dtos = Mapper.Map<List<VisitorDto>>(visitors);

            foreach (var dto in dtos)
            {
                dto.RegionIds = VisitorRepository.GetAllVisiting(dto.Id);
            }

            return dtos;
        }
    }
}
