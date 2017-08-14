using DataTransfer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Model;

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

        public async Task<TokenDto> Login(LoginViewModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8,"application/json");
            var response = await Client.PostAsync(GetUri(Config["Api:Login"]), content);
            TokenDto token = JsonConvert.DeserializeObject<TokenDto>(await response.Content.ReadAsStringAsync());

            return token;
        }

        public Uri GetUri(string action)
        {
            Uri uri = new Uri(Config["Api:Base"]);
            return new Uri(uri,action);
        }
    }
}
