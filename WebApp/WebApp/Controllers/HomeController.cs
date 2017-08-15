using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using DataTransfer;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationRoot Config;

        public HomeController(IConfigurationRoot config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
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
                var client = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var result = await client.PostAsync(new Uri(Config["Api:Base"] + Config["Api:Login"]), content);
                
                if (!result.IsSuccessStatusCode) ModelState.AddModelError(string.Empty, "Could not authenticate.");
                else
                {
                    var token = JsonConvert.DeserializeObject<TokenDto>(await result.Content.ReadAsStringAsync());
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
