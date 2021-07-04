using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace server
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        
        List<UserResponse> getUsersFromDb(int companyId)
        {
            //Get users from the database
            List<UserResponse> users = new List<UserResponse>();
            users.add(new UserResponse() { firstName = "First1", lastName = "Last1", address = "Address1", email = "Email1"});
            users.add(new UserResponse() { firstName = "First2", lastName = "Last2", address = "Address2", email = "Email2"});
            users.add(new UserResponse() { firstName = "First3", lastName = "Last3", address = "Address3", email = "Email3"});
            users.add(new UserResponse() { firstName = "First4", lastName = "Last4", address = "Address4", email = "Email4"});
            users.add(new UserResponse() { firstName = "First5", lastName = "Last5", address = "Address5", email = "Email5"});
            return users;
        }

        public override GetUsers(UserRequest request, IServerStreamWriter<UserResponse> responseStream,
         ServerCallContext context)
        {
            var users = getUsersFromDb();
        }
    }
}
