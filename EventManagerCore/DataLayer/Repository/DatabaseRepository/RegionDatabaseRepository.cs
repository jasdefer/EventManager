using Dapper;
using DataLayer.DataModel;
using DataLayer.Repository.DatabaseRepository.DatabaseExceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer.Repository.DatabaseRepository
{
    public class RegionDatabaseRepository : DatabaseRepository<Region, int>, IRegionRepository
    {
        public RegionDatabaseRepository(string connectionString) : base(connectionString)
        {
        }

        public RegionDatabaseRepository(IConfigurationRoot config) :base(config["ConnectionString"])
        {
            
        }

        public override string PropertiesString => "Name,Description,Polygon,Value,TimeStamp";

        public override string PropertiesStringAt => "@Name,@Description,@Polygon,@Value,@TimeStamp";

        public override string PropertiesStringUpdate => "Name=@Name,Description=@Description,Polygon=@Polygon,Value=@Value,TimeStamp=@TimeStamp";

        protected override string TableName => DatabaseContext.RegionsTableName;

        public void AddVisitor(int regionId, int visitorId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"Cannot find a region with the id {regionId}.");

            int affectedRows = 0;
            using (var sql = new SqlConnection(ConnectionString))
            {
                affectedRows = sql.Execute($"INSERT INTO {DatabaseContext.RegionVisitorsTableName} (RegionId,VisitorId) VALUES ({regionId},{visitorId});");
            }

            if (affectedRows < 1) throw new DatabaseException("No row was inserted.");
            if (affectedRows > 1) throw new DatabaseException($"Added {affectedRows} rows instead of 1.");
        }

        public IEnumerable<Region> GetAllAfter(DateTime time)
        {
            IEnumerable<Region> regions = null;
            using (var sql = new SqlConnection(ConnectionString))
            {
                regions = sql.Query<Region>($"Select * FROM {TableName} where TimeStamp > '{time}';");
            }

            return regions;
        }

        public IEnumerable<int> GetAllVisitors(int regionId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"Cannot find a region with the id {regionId}.");

            IEnumerable<int> visitorIds;
            using (var sql = new SqlConnection(ConnectionString))
            {
                visitorIds = sql.Query<int>($"Select VisitorId FROM {DatabaseContext.RegionVisitorsTableName} where RegionId={region.Id};");
            }

            return visitorIds;
        }

        public void RemoveAllVisitors(int regionId)
        {
            int affectedRows = 0;
            using (var sql = new SqlConnection(ConnectionString))
            {
                affectedRows = sql.Execute($"DELETE FROM {DatabaseContext.RegionVisitorsTableName} Where RegionId={regionId};");
            }
        }

        public void RemoveVisitor(int regionId, int visitorId)
        {
            int affectedRows = 0;
            using (var sql = new SqlConnection(ConnectionString))
            {
                affectedRows = sql.Execute($"DELETE FROM {DatabaseContext.RegionVisitorsTableName} Where RegionId={regionId} AND VisitorId={visitorId};");
            }

            if (affectedRows < 1) throw new DatabaseException("No row was deleted.");
            if (affectedRows > 1) throw new DatabaseException($"Deleted {affectedRows} rows instead of 1.");
        }
    }
}
