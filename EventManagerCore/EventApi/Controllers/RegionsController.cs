using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using DataTransfer;
using EventApi.Services.Filters;

namespace EventApi.Controllers
{
    
    [Route("api/[controller]")]
    [Authorize]
    [ValidateModelAttribute]
    public class RegionsController : Controller
    {
        private readonly RegionManager _regionManager;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(RegionManager regionManager,
            ILogger<RegionsController> logger)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}", Name = "RegionGet")]
        public IActionResult Get(int id)
        {
            try
            {
                RegionDto dto = _regionManager.Get(id);
                if (dto == null) return NotFound();
                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.LogWarning(1, e, $"Cannot get region {id}.");
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<RegionDto> regions = _regionManager.Get();
                return Ok(regions);
            }
            catch (Exception e)
            {
                _logger.LogWarning(1, e, "Cannot get all regions.");
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Add([FromBody]RegionDto dto)
        {
            try
            {
                dto.Id = _regionManager.Add(dto);
                var uri = Url.Link("RegionGet", new { id = dto.Id });
                return Created(uri,dto);
            }
            catch (Exception e)
            {
                _logger.LogWarning(1, e, "Cannot add region.");
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody]RegionDto dto)
        {
            try
            {
                _regionManager.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                else _logger.LogWarning(1, e, "Cannot update region");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _regionManager.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                else
                {
                    _logger.LogWarning(1, e, $"Cannot delete region {id}");
                }
            }

            return BadRequest();
        }
    }
}
