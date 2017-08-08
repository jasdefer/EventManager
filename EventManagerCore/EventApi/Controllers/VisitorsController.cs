using BusinessLayer;
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
    public class VisitorsController : Controller
    {
        private readonly VisitorManager VisitorManager;
        private readonly ILogger<RegionsController> Logger;

        public VisitorsController(VisitorManager visitorManager,
            ILogger<RegionsController> logger)
        {
            VisitorManager = visitorManager ?? throw new ArgumentNullException(nameof(visitorManager));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}", Name = "GetVisitor")]
        public IActionResult Get(int id)
        {
            try
            {
                VisitorDto dto = VisitorManager.Get(id);
                if (dto == null) return NotFound();
                return Ok(dto);
            }
            catch (Exception e)
            {
                Logger.LogWarning(1, e, $"Cannot get visitor {id}.");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<VisitorDto> visitors = VisitorManager.Get();
                return Ok(visitors);
            }
            catch (Exception e)
            {
                Logger.LogWarning(2, e, "Cannot get all visitors.");
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Add([FromBody]VisitorDto dto)
        {
            try
            {
                dto.Id = VisitorManager.Add(dto);
                var uri = Url.Link("GetVisitor", new { id = dto.Id });
                return Created(uri, dto);
            }
            catch (Exception e)
            {
                Logger.LogWarning(1, e, $"Cannot add visitor.");
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody]VisitorDto dto)
        {
            try
            {
                VisitorManager.Update(dto);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                Logger.LogWarning(1, e, $"Cannot update visitor.");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                VisitorManager.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException) return NotFound();
                Logger.LogWarning(2, e, $"Cannot delete visitor {id}.");
            }

            return BadRequest();
        }
    }
}
