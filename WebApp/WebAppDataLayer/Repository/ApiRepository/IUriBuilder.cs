using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository.ApiRepository
{
    public interface IUriBuilder<T> where T:class
    {
        Uri GetBaseUri();
    }
}
