using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDataLayer.Repository.ApiRepository;

namespace WebApp.Services.Middleware
{
    public class ReadToken
    {
        private readonly RequestDelegate _next;
        private readonly IConfigurationRoot _config;

        public ReadToken(RequestDelegate next, IConfigurationRoot config)
        {
            _next = next;
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task Invoke(HttpContext context, TokenAccessor tokenAccessor)
        {
            if (tokenAccessor == null) throw new ArgumentNullException(nameof(tokenAccessor));

            tokenAccessor.SetToken(context.Request.Cookies[_config["Cookie:Token"]]);
            await _next(context);
        }
    }
}
