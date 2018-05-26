using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.App.Data.Models
{
    public class Skill
    {
        public long SkillId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public int Strength { get; set; }
    }
}
