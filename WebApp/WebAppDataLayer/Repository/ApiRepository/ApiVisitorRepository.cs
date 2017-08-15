using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository.ApiRepository
{
    public class ApiVisitorRepository : ApiRepository<VisitorDto, int>, IVisitorRepository
    {
        public ApiVisitorRepository(TokenAccessor tokenAccessor, IUriBuilder<VisitorDto> uriBuilder) : base(tokenAccessor, uriBuilder)
        {
        }
    }
}
