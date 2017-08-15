using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppDataLayer.Repository.ApiRepository.ApiRepositoryExceptions;

namespace WebAppDataLayer.Repository.ApiRepository
{
    public class ApiRegionRepository : ApiRepository<RegionDto, int>, IRegionRepository
    {
        public ApiRegionRepository(TokenAccessor tokenAccessor, IUriBuilder<RegionDto> uriBuilder) : base(tokenAccessor, uriBuilder)
        {
        }

        public async Task RemoveVisitorAsync(int regionId, int visitorId)
        {
            var response = await Client.DeleteAsync($"RemoveVisitor/{regionId}/{visitorId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new UnexpectedServerResponse(response.StatusCode);
            }
        }
    }
}
