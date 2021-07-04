using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace server
{
    public class UserService : Users.UsersBase
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
    
            users.Add(new UserResponse() { FirstName = "First1", LastName = "Last1", Address = "Address1", Email = "Email1"});
            users.Add(new UserResponse() { FirstName = "First2", LastName = "Last2", Address = "Address2", Email = "Email2"});
            users.Add(new UserResponse() { FirstName = "First3", LastName = "Last3", Address = "Address3", Email = "Email3"});
            users.Add(new UserResponse() { FirstName = "First4", LastName = "Last4", Address = "Address4", Email = "Email4"});
            users.Add(new UserResponse() { FirstName = "First5", LastName = "Last5", Address = "Address5", Email = "Email5"});
            return users;
        }

        public override async Task GetUsers(UserRequest request, IServerStreamWriter<UserResponse> responseStream,
         ServerCallContext context)
        {
            var users = getUsersFromDb(request.CompanyId);
            foreach(UserResponse user in users)
            {
                //emulate a long running proccess
                await Task.Delay(1000);
                await responseStream.WriteAsync(user);
            }
        }
    }
}
