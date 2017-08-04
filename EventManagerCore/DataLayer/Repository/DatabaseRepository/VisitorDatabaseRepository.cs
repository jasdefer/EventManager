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

        protected override string TableName => "Visitors";
    }
}
