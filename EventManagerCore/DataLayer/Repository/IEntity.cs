using System;

namespace DataLayer.Repository
{
    public interface IEntity<TU> where TU: IComparable
    {
        TU Id { get; set; }
    }
}
