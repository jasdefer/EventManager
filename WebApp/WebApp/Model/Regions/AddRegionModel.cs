using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Model.Regions
{
    public class AddRegionModel
    {
        [Range(0, 10)]
        public int? Value { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(11)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Range(typeof(DateTime), minimum: "2000-01-01", maximum: "2099-12-31")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1024)]
        public string Polygon { get; set; }
    }
}
