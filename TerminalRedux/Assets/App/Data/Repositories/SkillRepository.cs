using Assets.App.Data.Models;
using Assets.AppFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Assets.App.Data.Repositories
{
    public class SkillRepository : Repository<Skill>
    {
        public SkillRepository(string connectionString) 
            : base(connectionString)
        {
        }

        public IEnumerable<Skill> Get()
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Skill";
                return ToList(command);
            }
        }

        public IEnumerable<Skill> GetByPackageId(long packageId)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT s.* 
                FROM Skill s
                JOIN PackageSkill ps
                ON s.SkillId = ps.SkillId
                WHERE ps.PackageId = @PackageId";

                command.AddParameter("PackageId", packageId);
                return ToList(command);
            }
        }

        protected override void Map(IDataRecord record, Skill entity)
        {
            entity.SkillId = (long)record["SkillId"];
            entity.Name = (string)record["Name"];
            entity.Cost = (int)record["Cost"];
            entity.Strength = (int)record["Strength"];
        }
    }
}
