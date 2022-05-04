using API.Automation.Authenticator;
using API.Automation.Models;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation
{
    public class APIClient : IAPIClient, IDisposable
    {
        readonly RestClient _client;
        readonly string BASE_URL = "https://reqres.in"; 
        readonly string a = ConfigurationManager.AppSettings["BASE_URL"].;
        
        public APIClient(string apiKey = "", string apiSecret = "" )
        {
            var options = new RestClientOptions(BASE_URL);
            _client = new RestClient(options)
            {
                Authenticator = new APIAuthenticator()
            };
        }

        public async Task<RestResponse> GetUser(int pageNumber = 2)
        {
            var request = new RestRequest(Endpoints.LIST_USERS)
                .AddParameter("page", pageNumber);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> CreateUser<T>(T payload) where T : class
        {
            var request = new RestRequest(Endpoints.CREATE_USER);
            request.AddBody(payload);
            request.RequestFormat = DataFormat.Json;
            return await _client.ExecuteAsync<CreateUserRes>(request, Method.Post);
        }

        public async Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class
        {
            var request = new RestRequest(Endpoints.Update_USER + id);
            request.AddBody(payload);
            request.RequestFormat = DataFormat.Json;
            return await _client.ExecuteAsync<CreateUserRes>(request, Method.Put);
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
