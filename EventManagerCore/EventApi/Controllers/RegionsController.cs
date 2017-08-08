using BusinessLayer;
using BusinessLayer.BusinessExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.Dto;

namespace EventApi.Controllers
{
    [Route("api/[controller]")]
    public class RegionsController : Controller
    {
        private readonly RegionManager RegionManager;
        private readonly ILogger<RegionsController> Logger;

        public RegionsController(RegionManager regionManager,
            ILogger<RegionsController> logger)
        {
            RegionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}", Name = "RegionGet")]
        public IActionResult Get(int id)
        {
            try
            {
                RegionDto dto = RegionManager.Get(id);
                if (dto == null) return NotFound();
                return Ok(dto);
            }
            catch (Exception e)
            {
                Logger.LogWarning(1, e, $"Cannot get region {id}.");
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<RegionDto> regions = RegionManager.Get();
                return Ok(regions);
            }
            catch (Exception e)
            {
                Logger.LogWarning(1, e, "Cannot get all regions.");
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Add([FromBody]RegionDto dto)
        {
            try
            {
                dto.Id = RegionManager.Add(dto);
                var uri = Url.Link("RegionGet", new { id = dto.Id });
                return Created(uri,dto);
            }
            catch (Exception e)
            {
                Logger.LogWarning(1, e, $"Cannot add region.");
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody]RegionDto dto)
        {
            try
            {
                RegionManager.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                else Logger.LogWarning(1, e, $"Cannot update region");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                RegionManager.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                else
                {
                    Logger.LogWarning(1, e, $"Cannot delete region {id}");
                }
            }

            return BadRequest();
        }
    }
}
