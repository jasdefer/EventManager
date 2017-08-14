using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Model;
using WebApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Api Api;
        private readonly IConfigurationRoot Config;

        public HomeController(Api api, IConfigurationRoot config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Api = api ?? throw new ArgumentNullException(nameof(config));
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View(new LoginViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var token = await Api.Login(model);
                if (token == null) ModelState.AddModelError(string.Empty, "Could not authenticate.");
                else
                {
                    CookieOptions options = new CookieOptions() { HttpOnly = true, Expires = token.Expiration };
                    Response.Cookies.Append(Config["Cookie:Token"], token.Value, options);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(Config["Cookie:Token"]);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
