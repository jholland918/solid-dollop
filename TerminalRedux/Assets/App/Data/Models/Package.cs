using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.App.Data.Models
{
    public class Package
    {
        public long PackageId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<Skill> Skills { get; set; }
    }
}
