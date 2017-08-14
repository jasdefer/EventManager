using DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;
using WebApp.Services.WebAppExceptions;

namespace WebApp.Services
{
    public class Api
    {
        private readonly IConfigurationRoot Config;
        private readonly HttpClient Client;

        public Api(IConfigurationRoot config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Client = new HttpClient();
        }

        public async Task<IEnumerable<VisitorDto>> GetVisitors(IRequestCookieCollection cookies)
        {
            return await Get<IEnumerable<VisitorDto>>(GetUri(Config["Api:Visitors"]), cookies);
        }

        public async Task<IEnumerable<RegionDto>> GetRegions(IRequestCookieCollection cookies)
        {
            return await Get<IEnumerable<RegionDto>>(GetUri(Config["Api:Regions"]), cookies);
        }

        public async Task<TokenDto> Login(LoginViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8,"application/json");
            var response = await Client.PostAsync(GetUri(Config["Api:Login"]), content);
            return await ValidateResponse<TokenDto>(response);
        }

        public Uri GetUri(string action)
        {
            Uri uri = new Uri(Config["Api:Base"]);
            return new Uri(uri,action);
        }

        private async Task<T> Get<T>(Uri uri, IRequestCookieCollection cookies) where T:class
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", cookies[Config["Cookie:Token"]]);
            var response = await Client.GetAsync(uri);
            return await ValidateResponse<T>(response);
        }

        private async Task<T> ValidateResponse<T>(HttpResponseMessage response) where T : class
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return null;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new ServerErrorException();
            }

            throw new UnexpectedServerResponse(response.StatusCode);

        }
    }
}
