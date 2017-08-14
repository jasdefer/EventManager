using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly Api Api;

        public VisitorsController(Api api)
        {
            Api = api ?? throw new ArgumentNullException(nameof(api));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var regions = await Api.GetVisitors(Request.Cookies);
            return View(regions);
        }
    }
}
