using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class RegionsController : Controller
    {
        private readonly Api Api;

        public RegionsController(Api api)
        {
            Api = api ?? throw new ArgumentNullException(nameof(api));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var regions = await Api.GetRegions(Request.Cookies);
            return View(regions);
        }
    }
}
