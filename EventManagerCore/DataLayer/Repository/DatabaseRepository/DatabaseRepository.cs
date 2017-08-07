using Dapper;
using DataLayer.Repository.DatabaseRepository.DatabaseExceptions;
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
        protected string ConnectionString;
        protected abstract string TableName { get; }
        public abstract string PropertiesString { get; }
        public abstract string PropertiesStringAt { get; }
        public abstract string PropertiesStringUpdate { get; }

        public DatabaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(connectionString);
            DatabaseContext.CreateDb(ConnectionString);
        }

        public T Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using (var sql = new SqlConnection(ConnectionString))
            {
                U id = sql.ExecuteScalar<U>($"INSERT INTO {TableName} ({PropertiesString}) VALUES ({PropertiesStringAt});SELECT SCOPE_IDENTITY();",entity);
                entity.Id = id;
            }
            return entity;
        }

        public void Delete(U id)
        {
            T entity = Get(id);
            if (entity == null) throw new KeyNotFoundException($"Cannot find the entity with the id {id} in {TableName}");

            int affectedRows = 0;
            using (var sql = new SqlConnection(ConnectionString))
            {
                affectedRows = sql.Execute($"DELETE FROM {TableName} where Id={entity.Id};");
            }

            if (affectedRows < 1) throw new DatabaseException("No row was deleted.");
            if (affectedRows > 1) throw new DatabaseException($"Deleted {affectedRows} rows instead of 1.");
        }

        public T Get(U id)
        {
            T entity = default(T);
            using (var sql = new SqlConnection(ConnectionString))
            {
                entity = sql.QueryFirstOrDefault<T>($"select * from {TableName} where Id=${id};");
            }

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = null;
            using (var sql = new SqlConnection(ConnectionString))
            {
                entities = sql.Query<T>($"select * from {TableName};");
            }

            return entities;
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            int affectedRows = 0;
            using (var sql = new SqlConnection(ConnectionString))
            {
                affectedRows = sql.Execute($"Update {TableName} SET {PropertiesStringUpdate} where Id={entity.Id};",entity);
            }

            if (affectedRows < 1) throw new DatabaseException("No row was updated.");
            if (affectedRows > 1) throw new DatabaseException($"Updated {affectedRows} rows instead of 1.");
        }
    }
}
