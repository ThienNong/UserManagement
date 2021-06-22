using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models.Cassandra
{
    public class User
    {
        public string UID { get; set; }
        public string password { get; set; }

        public User(string UID, string password)
        {
            this.UID = UID;
            this.password = password;
        }
    }
}
