using DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.DatabaseRepository
{
    public class RegionDatabaseRepository : DatabaseRepository<Region, int>
    {
        public RegionDatabaseRepository(string connectionString) : base(connectionString)
        {
        }

        public override string PropertiesString => "Name,Description,Polygon,Value,TimeStamp";

        public override string PropertiesStringAt => "@Name,@Description,@Polygon,@Value,@TimeStamp";

        public override string PropertiesStringUpdate => "Name=@Name,Description=@Description,Polygon=@Polygon,Value=@Value,TimeStamp=@TimeStamp";

        protected override string TableName => "Regions";
    }
}
