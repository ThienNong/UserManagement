using Cassandra;
using Cassandra.Data;
using Cassandra.Mapping;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Models.Cassandra;

namespace UserManagement.Services.BLL
{
    public class UserService
    {
        CassandraConnectorServices cassandraConnectorServices;
        public UserService()
        {
            cassandraConnectorServices = new CassandraConnectorServices();
        }
        public bool isValid(string uid)
        {
            return GetUser(uid) != null;
        }

        public List<User> GetAllUsers()
        {
            string sql = "SELECT * FROM user";
            using (ISession session = cassandraConnectorServices.Cluster().Connect())
            {
                IMapper mapper = new Mapper(session);

                List<User> data = mapper.Fetch<User>(sql).ToList();

                if (data.Count() == 0)
                    return null;
                return data;
            }
        }

        public User GetUser(string uid)
        {
            string sql = "SELECT * FROM User WHERE uid = '" + uid + "'";
            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);

                var data = mapper.Fetch<User>(sql).FirstOrDefault();

                if (data == null || data.UID == "")
                    return null;

                return data;
            }
        }

        public User Add(User obj)
        {
            if (isValid(obj.UID))
                return null;

            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                var mapper = new Mapper(session);
                mapper.Insert(obj);
                mapper.Insert(new UserProfile("", "", "", "", "", obj.UID));
                return obj;
            }
        }

        public User Update(User obj)
        {
            if (!isValid(obj.UID))
                return null;

            using (var session = cassandraConnectorServices.Cluster().Connect())
            {
                //new Mapper(session).Update<User>("SET password = ? WHERE uid = ?", obj.password, obj.UID);
                new Mapper(session).Update(obj);
                return obj;
            }
        }

        public User Delete(User obj)
        {
            if (!isValid(obj.UID))
                return null;

            string connectionString = "Contact Points=localhost; Default Keyspace=studentmanagement";

            using (CqlConnection connection = new CqlConnection(connectionString))
            {
                connection.Open();

                CqlBatchTransaction transaction = new CqlBatchTransaction(connection);

                try
                {
                    using (var session = cassandraConnectorServices.Cluster().Connect())
                    {
                        var mapper = new Mapper(session);
                        mapper.Delete(obj);
                        mapper.Delete<UserProfile>("WHERE UID = ?", obj.UID);
                        transaction.Commit();
                    }
                }
                catch
                {
                    transaction.Rollback();
                    return null;
                }
            }
            return obj;
        }

        public User Delete2(User obj)
        {
            if (!isValid(obj.UID))
                return null;

            string connectionString = "Contact Points=localhost; Default Keyspace=usermanagement";

            using (CqlConnection connection = new CqlConnection(connectionString))
            {
                var transation = connection.BeginTransaction();
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM User WHERE UID = '" + obj.UID + "'";
                    command.ExecuteNonQuery();
                    transation.Commit();
                }
                catch
                {
                    transation.Rollback();
                    return null;
                }
            }
            return obj;
        }
    }
}
