using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models.Cassandra
{
    public class UserProfile
    {
        public string email { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string sex { get; set; }
        public string UID { get; set; }

        public UserProfile(string email, string fullName, string phoneNumber, string address, string sex, string UID)
        {
            this.email = email;
            this.fullName = fullName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.sex = sex;
            this.UID = UID;
        }
    }
}
