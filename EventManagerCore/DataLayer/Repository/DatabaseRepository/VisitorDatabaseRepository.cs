using DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.DatabaseRepository
{
    public class VisitorDatabaseRepository : DatabaseRepository<Visitor, int>
    {
        public VisitorDatabaseRepository(string connectionString) : base(connectionString)
        {
        }

        public override string PropertiesString => "Username,Email,PasswordHash,Bio";

        public override string PropertiesStringAt => "@Username,@Email,@PasswordHash,@Bio";

        public override string PropertiesStringUpdate => "Username=@Username,Email=@Email,PasswordHash=@PasswordHash,Bio=@Bio";

        protected override string TableName => "Visitors";
    }
}
