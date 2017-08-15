using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository.ApiRepository
{
    public class ApiRegionRepository : ApiRepository<RegionDto, int>, IRegionRepository
    {
        public ApiRegionRepository(TokenAccessor tokenAccessor, IUriBuilder<RegionDto> uriBuilder) : base(tokenAccessor, uriBuilder)
        {
        }
    }
}
