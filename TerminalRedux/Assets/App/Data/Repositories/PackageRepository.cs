using Assets.App.Data.Models;
using Assets.AppFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Assets.App.Data.Repositories
{
    public class PackageRepository : Repository<Package>
    {
        private SkillRepository _skillRepository;

        public PackageRepository(string connectionString) 
            : base(connectionString)
        {
            _skillRepository = new SkillRepository(connectionString);
        }

        public Package Get(int id)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Package WHERE PackageId = @PackageId";
                command.AddParameter("PackageId", id);
                var record =  Single(command);
                record.Skills = _skillRepository.GetByPackageId(record.PackageId);
                return record;
            }
        }

        public IEnumerable<Package> GetByUserId(int userId)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Package WHERE UserId = @UserId";
                command.AddParameter("UserId", userId);
                return ToList(command);
            }
        }

        protected override void Map(IDataRecord record, Package entity)
        {
            entity.PackageId = (long)record["PackageId"];
            entity.UserId = (int)record["UserId"];
            entity.Name = (string)record["Name"];
            entity.CreatedAt = DateTime.Parse((string)record["CreatedAt"]);
        }
    }
}
