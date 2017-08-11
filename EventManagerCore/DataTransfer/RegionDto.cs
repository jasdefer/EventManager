using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataTransfer
{
    public class RegionDto
    {
        public int Id { get; set; }

        [Range(0,10)]
        public int? Value { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(11)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Range(typeof(DateTime),minimum: "2000-01-01",maximum:"2099-12-31")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1024)]
        public string Polygon { get; set; }

        public IEnumerable<int> VisitorIds { get; set; }
    }
}
