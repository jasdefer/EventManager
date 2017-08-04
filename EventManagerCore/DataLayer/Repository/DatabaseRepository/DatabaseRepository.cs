using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.DatabaseRepository
{
    public abstract class DatabaseRepository<T, U> : IRepository<T, U> where T : IEntity<U> where U : IComparable
    {
        private string ConnectionString;
        protected abstract string TableName { get; }

        public DatabaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(connectionString);
        }

        public T Add(T entity)
        {
            
            throw new NotImplementedException();
        }

        public void Delete(U id)
        {
            throw new NotImplementedException();
        }

        public T Get(U id)
        {
            T entity = default(T);
            using (var sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                entity = sql.QueryFirstOrDefault<T>($"select * from {TableName} where Id=${id};");
                sql.Close();
            }

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = null;
            using (var sql = new SqlConnection(ConnectionString))
            {
                sql.Open();
                entities = sql.Query<T>($"select * from {TableName};");
                sql.Close();
            }

            return entities;
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
