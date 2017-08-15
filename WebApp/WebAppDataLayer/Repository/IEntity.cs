using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository
{
    public interface IEntity<TU> where TU : IComparable
    {
        int Id { get; set; }
    }
}
