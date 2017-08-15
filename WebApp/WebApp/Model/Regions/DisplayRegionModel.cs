using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model.Regions
{
    public class DisplayRegionModel
    {
        public int Id { get; set; }

        public int? Value { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Polygon { get; set; }

        public List<VisitorDto> Visitors { get; set; }
    }
}
