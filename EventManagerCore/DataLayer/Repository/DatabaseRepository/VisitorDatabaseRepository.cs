using Dapper;
using DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.DatabaseRepository
{
    public class VisitorDatabaseRepository : DatabaseRepository<Visitor, int>, IVistorRepository
    {
        public VisitorDatabaseRepository(string connectionString) : base(connectionString)
        {
        }

        public override string PropertiesString => "Username,Email,PasswordHash,Bio";

        public override string PropertiesStringAt => "@Username,@Email,@PasswordHash,@Bio";

        public override string PropertiesStringUpdate => "Username=@Username,Email=@Email,PasswordHash=@PasswordHash,Bio=@Bio";

        protected override string TableName => "Visitors";

        public IEnumerable<int> GetAllVisiting(int visitorId)
        {
            Visitor visitor = Get(visitorId);
            if (visitor == null) throw new KeyNotFoundException($"Cannot find the visitor with the id {visitorId}.");

            IEnumerable<int> regionIds;
            using (var sql = new SqlConnection(ConnectionString))
            {
                regionIds = sql.Query<int>($"SELECT RegionId FROM {DatabaseContext.RegionVisitorsTableName} Where VisitorId={visitor.Id}");
            }

            return regionIds;
        }
    }
}
