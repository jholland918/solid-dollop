using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Assets.AppFramework.Data
{
    public abstract class Repository<TEntity> : IDisposable
        where TEntity : new()
    {
        protected readonly IDbConnection connection;

        public Repository(string connectionString)
        {
            connection = new SqliteConnection(connectionString);
            connection.Open();
        }

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<TEntity> items = new List<TEntity>();
                while (reader.Read())
                {
                    var item = new TEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }

        protected TEntity Single(IDbCommand command)
        {
            return ToList(command).FirstOrDefault();
        }

        protected abstract void Map(IDataRecord record, TEntity entity);

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
