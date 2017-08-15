using DataTransfer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDataLayer.Repository.ApiRepository;

namespace WebApp.Services.UriBuilder
{
    public class RegionUri : IUriBuilder<RegionDto>
    {
        private readonly IConfigurationRoot Config;

        public RegionUri(IConfigurationRoot config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public Uri GetBaseUri()
        {
            return new Uri(Config["Api:Base"] + Config["Api:Regions"]);
        }
    }
}
