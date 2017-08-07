using Dapper;
using DataLayer.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.DatabaseRepository
{
    public class DatabaseContext
    {
        public const string RegionsTableName = "Regions";
        public const string VisitorsTableName = "Visitors";
        public const string RegionVisitorsTableName = "RegionVisitors";

        /// <summary>
        /// Create the database if it does not exist already.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="path">The path where the database will be created.</param>
        public static void CreateDb(string connectionString, string path = null)
        {
            path = path ?? Path.GetTempPath();

            //Get the name of the database from the connection string
            var builder = new SqlConnectionStringBuilder(connectionString);
            string dbName = builder.InitialCatalog;

            //Check if the database exist. Use the master connection of the database for this operation
            bool exists = false;
            builder.InitialCatalog = "master";
            using (var sql = new SqlConnection(builder.ConnectionString))
            {
                exists = sql.QuerySingle<bool>(string.Format(Resources.DoesDbExists,dbName));
                if (!exists)
                {
                    //Create the database
                    sql.Execute(string.Format(Resources.CreateDatabase, dbName, path));
                }
            }

            //Create the tables for the database. Use the connection to the actual database itself this time
            if (!exists)
            {
                using (var sql = new SqlConnection(connectionString))
                {
                    sql.Execute(string.Format(Resources.CreateTables, dbName));
                }
            }
        }
    }
}
