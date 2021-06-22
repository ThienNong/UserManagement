using Cassandra;
using Cassandra.Mapping;
using UserManagement.Models.Cassandra;

namespace UserManagement.Services
{
    public class CassandraConnectorServices
    {
        static CassandraConnectorServices()
        {
            MappingConfiguration.Global.Define(
                new Map<User>().TableName("user").PartitionKey(user => user.UID)
            );
            MappingConfiguration.Global.Define(
                new Map<UserProfile>().TableName("userprofile").PartitionKey(userProfile => userProfile.UID)
            );
        }

        public ICluster Cluster()
        {
            return new Builder().AddContactPoint("localhost").WithPort(9042)
                .WithDefaultKeyspace("usermanagement").Build();
        }
    }
}
