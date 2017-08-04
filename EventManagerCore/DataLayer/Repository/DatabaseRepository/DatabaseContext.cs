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

        public static void CreateDb(string connectionString, string dbName, string path)
        {
            var bla = Resources.ResourceManager.GetString("SeedTestDatabase");

            using (var sql = new SqlConnection(connectionString))
            {
                bool exists = sql.QuerySingle<bool>($"If(db_id(N'{dbName}') IS NULL) select 0 else select 1;");
                if (!exists)
                {

                }
            }
        }
    }
}
