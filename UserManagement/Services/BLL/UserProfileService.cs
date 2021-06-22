
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models.Cassandra;
using Cassandra.Mapping;

namespace UserManagement.Services.BLL
{
    public class UserProfileService
    {
        CassandraConnectorServices cassandraConnectorServices;
        public UserProfileService()
        {
            cassandraConnectorServices = new CassandraConnectorServices();
        }
        public bool isValid(string uid)
        {
            return GetUserProfile(uid) != null;
        }

        public List<UserProfile> GetAllUsersProfile()
        {
            string sql = "SELECT * FROM userprofile";
            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);

                var data = mapper.Fetch<UserProfile>(sql).ToList();

                if (data.Count() == 0)
                    return null;
                return data;
            }
        }
        public UserProfile GetUserProfile(string UID)
        {
            string sql = "SELECT * FROM userprofile WHERE uid = '" + UID + "'";
            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);

                var data = mapper.Fetch<UserProfile>(sql).FirstOrDefault();

                if (data == null || data.UID == "")
                    return null;
                return data;
            }
        }
        public UserProfile Add(UserProfile obj)
        {
            if (isValid(obj.UID))
                return null;

            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);
                mapper.Insert(obj);
                return obj;
            }
        }
        public UserProfile Update(UserProfile obj)
        {
            if (!isValid(obj.UID))
                return null;

            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);
                mapper.Update(obj);
                return obj;
            }
        }
        public UserProfile Delete(UserProfile obj)
        {
            if (isValid(obj.UID))
                return null;

            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);
                mapper.Delete(obj);
                return obj;
            }
        }
    }
}
