using API.Automation.Models;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Automation
{
    public interface IAPIClient
    {
        Task<RestResponse> GetUser(int pageNumber);
        Task<RestResponse> CreateUser<T>(T payload) where T: class;
        Task<RestResponse> UpdateUser<T>(T payload, string id) where T : class;
    }
}
