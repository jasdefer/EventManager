using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.PropertyValidation;

namespace DataLayer.DataModel
{
    public class Visitor : IEntity<int>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }

        public IEnumerable<Region> Regions { get; set; }
    }
}
