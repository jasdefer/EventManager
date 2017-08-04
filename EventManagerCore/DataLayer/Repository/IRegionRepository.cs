﻿using DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRegionRepository : IRepository<Region, int>
    {
        IEnumerable<Region> GetAllAfter(DateTime time);
        IEnumerable<Region> GetAllVisiting(Visitor visitor);
    }
}
