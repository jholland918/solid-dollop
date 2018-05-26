using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.App.Data.Models
{
    public class User
    {
        public long UserId { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
