using AutoMapper;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model.Regions;
using WebAppDataLayer.Repository;

namespace WebApp.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IRegionRepository RegionRepository;
        private readonly IMapper _mapper;
        private readonly IVisitorRepository _visitorRepository;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, IVisitorRepository visitorRepository)
        {
            RegionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _visitorRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var regions = await RegionRepository.GetAllAsync();
            return View(regions);
        }

        [HttpGet]
        public async Task<IActionResult> Display(int id)
        {
            RegionDto region = await RegionRepository.GetAsync(id);
            DisplayRegionModel model = _mapper.Map<DisplayRegionModel>(region);
            if (region.VisitorIds != null)
            {
                List<VisitorDto> visitors = new List<VisitorDto>();
                foreach (var visitorId in region.VisitorIds)
                {
                    visitors.Add(await _visitorRepository.GetAsync(visitorId));
                }
                model.Visitors = visitors;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await RegionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveVisitor(int regionId, int visitorId)
        {
            await RegionRepository.RemoveVisitorAsync(regionId, visitorId);
            return RedirectToAction(nameof(Display), new { id = regionId });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new RegionDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionModel model)
        {
            if (ModelState.IsValid)
            {
                var region = await RegionRepository.AddAsync(_mapper.Map<RegionDto>(model));
                return RedirectToAction(nameof(Display), new { id = region.Id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            RegionDto dto = await RegionRepository.GetAsync(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RegionDto dto)
        {
            if (ModelState.IsValid)
            {
                await RegionRepository.UpdateAsync(dto);
                return RedirectToAction(nameof(Display), new { id = dto.Id });
            }

            return View(dto);
        }
    }
}
