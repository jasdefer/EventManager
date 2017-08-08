﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IDataSeeder
    {
        RegionManager RegionManager { get; }
        VisitorManager VisitorManager { get; }
        void SeedData();
    }
}
