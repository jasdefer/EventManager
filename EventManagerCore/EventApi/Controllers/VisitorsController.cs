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
    [Authorize]
    [Route("api/[controller]")]
    [ValidateModelAttribute]
    public class VisitorsController : Controller
    {
        private readonly VisitorManager _visitorManager;
        private readonly ILogger<RegionsController> _logger;

        public VisitorsController(VisitorManager visitorManager,
            ILogger<RegionsController> logger)
        {
            _visitorManager = visitorManager ?? throw new ArgumentNullException(nameof(visitorManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}", Name = "GetVisitor")]
        public IActionResult Get(int id)
        {
            try
            {
                VisitorDto dto = _visitorManager.Get(id);
                if (dto == null) return NotFound();
                return Ok(dto);
            }
            catch (Exception e)
            {
                _logger.LogWarning(1, e, $"Cannot get visitor {id}.");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<VisitorDto> visitors = _visitorManager.Get();
                return Ok(visitors);
            }
            catch (Exception e)
            {
                _logger.LogWarning(2, e, "Cannot get all visitors.");
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody]VisitorDto dto)
        {
            try
            {
                _visitorManager.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                _logger.LogWarning(1, e, "Cannot update visitor.");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _visitorManager.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                _logger.LogWarning(2, e, $"Cannot delete visitor {id}.");
            }

            return BadRequest();
        }
    }
}
