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
        public Visitor(string username, string email)
        {
            Username = username;
            Email = email;

            VisitorValidation.Username.IsValid(Username);
            VisitorValidation.Email.IsValid(Email);
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }

        public IEnumerable<Region> Regions { get; set; }
    }
}
