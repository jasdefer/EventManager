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
    public class VisitorUri : IUriBuilder<VisitorDto>
    {
        private readonly IConfigurationRoot Config;

        public VisitorUri(IConfigurationRoot config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }
        public Uri GetBaseUri()
        {
            return new Uri(Config["Api:Base"] + Config["Api:Visitors"]);
        }
    }
}
