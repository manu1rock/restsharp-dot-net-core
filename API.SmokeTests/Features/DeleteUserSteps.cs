using API.Automation;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utilities;
using API.SmokeTests.Hook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.SmokeTests.Features
{
    [Binding]
    public class DeleteUserSteps
    {
        private CreateUserReq createUser;
        private RestResponse response;
        private HttpStatusCode statusCode;

        public DeleteUserSteps(CreateUserReq createUser)
        {
            this.createUser = createUser;
        }

        [Given(@"I created a user:")]
        public async Task GivenICreatedAUser(Table table)
        {
            DataTable user = TableExtensions.ToDataTable(table);
            createUser.name = user.Rows[0]["Name"].ToString();
            createUser.job = user.Rows[0]["Job"].ToString();

            var api = new APIClient(Hooks.config.BaseUrl, Hooks.config.Token);
            response = await api.CreateUser<CreateUserReq>(createUser);
            statusCode = response.get_StatusCode();
            var code = (int)statusCode;
            Assert.AreEqual(201, code);

            var content = HandleContent.GetContent<CreateUserRes>(response);
            Assert.AreEqual(createUser.name, content.name);
            Assert.AreEqual(createUser.job, content.job);
        }
        
        [When(@"I send request to delete user")]
        public void WhenISendRequestToDeleteUser()
        {
            
        }
        
        [Then(@"validate user is deleted")]
        public void ThenValidateUserIsDeleted()
        {
            
        }
    }
}
