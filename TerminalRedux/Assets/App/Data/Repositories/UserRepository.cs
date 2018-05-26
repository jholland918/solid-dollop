using Assets.App.Data.Models;
using Assets.AppFramework.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Assets.App.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(string connectionString) 
            : base(connectionString)
        {
        }

        public IEnumerable<User> Get()
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM User";
                return ToList(command);
            }
        }

        public IEnumerable<User> FindUsers(string username)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM User WHERE UPPER(Username) = UPPER(@Username)";
                command.AddParameter("Username", username);
                return ToList(command);
            }
        }

        public void Create(User user)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"INSERT INTO User (Username, CreatedAt) VALUES(@Username, @CreatedAt);SELECT last_insert_rowid();";
                command.AddParameter("Username", user.Username);
                command.AddParameter("CreatedAt", user.CreatedAt);

                //command.ExecuteNonQuery();
                user.UserId = (long)command.ExecuteScalar();

            }
        }

        public void Update(User user)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE User SET Username = @Username WHERE UserId = @UserId";
                command.AddParameter("Username", user.Username);
                command.AddParameter("UserId", user.UserId);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(long id)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"DELETE FROM User WHERE UserId = @UserId";
                command.AddParameter("UserId", id);
                command.ExecuteNonQuery();
            }
        }

        protected override void Map(IDataRecord record, User entity)
        {
            entity.UserId = (long)record["UserId"];
            entity.Username = (string)record["Username"];
            entity.CreatedAt = DateTime.Parse((string)record["CreatedAt"]);
        }
    }
}
