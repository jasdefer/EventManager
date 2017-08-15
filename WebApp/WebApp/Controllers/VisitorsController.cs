using AutoMapper;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model.Visitors;
using WebAppDataLayer.Repository;
using WebAppDataLayer.Repository.ApiRepository.ApiRepositoryExceptions;

namespace WebApp.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly IVisitorRepository _visitorRepository;
        private IMapper _mapper;

        public VisitorsController(IVisitorRepository visitorRepository, IMapper mapper)
        {
            _visitorRepository = visitorRepository ?? throw new ArgumentNullException(nameof(visitorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var visitors = await _visitorRepository.GetAllAsync();
            return View(visitors);
        }

        [HttpGet]
        public async Task<IActionResult> Display(int id)
        {
            VisitorDto visitor = await _visitorRepository.GetAsync(id);
            return View(visitor);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _visitorRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            VisitorDto dto = await _visitorRepository.GetAsync(id);
            return View(Mapper.Map<UpdateVisitorModel>(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateVisitorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VisitorDto dto = await _visitorRepository.GetAsync(model.Id);
                    await _visitorRepository.UpdateAsync(_mapper.Map(model,dto));
                    return RedirectToAction(nameof(Display), new { id = model.Id });
                }
                catch (UnexpectedServerResponse e)
                {
                    if(e.StatusCode == HttpStatusCode.BadRequest)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid data.");
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }

            return View(model);
        }
    }
}
