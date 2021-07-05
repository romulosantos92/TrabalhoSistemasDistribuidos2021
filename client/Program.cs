using System;
using System.Net.Http;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using server;

namespace client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Consuming a GRPC service");
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient});
            var client = new Users.UsersClient(channel);
            try
            {
                UserRequest request = new UserRequest() { CompanyId = 1 };
                using (var call = client.GetUsers(request))
                {
                    //stream
                    while( await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var currentUser = call.ResponseStream.Current;
                        Console.WriteLine(currentUser.FirstName + " " + currentUser.LastName + " is being fetched by the service");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
