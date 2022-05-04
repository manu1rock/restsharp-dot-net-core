using API.Automation;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace API.SmokeTests.Features
{
    [Binding]
    public class E2ETestSteps
    {
        private readonly CreateUserReq createUser;
        private RestResponse response;
        private HttpStatusCode statusCode;
        private readonly APIClient api;
        private readonly ScenarioContext scenarioContext;

        public E2ETestSteps(CreateUserReq createUser, ScenarioContext scenarioContext)
        {
            this.createUser = createUser;
            this.api = new APIClient();
            this.scenarioContext = scenarioContext;
        }

        [Given(@"I created a user with Name '(.*)' and Job '(.*)'")]
        public async Task GivenICreatedAUserWithNameAndJob(string name, string job)
        {
            createUser.name = name;
            createUser.job = job;

            response = await api.CreateUser<CreateUserReq>(createUser);
            statusCode = response.get_StatusCode();
            var code = (int)statusCode;
            Assert.AreEqual(201, code);
            var content = HandleContent.GetContent<CreateUserRes>(response);
            scenarioContext.Add("UserId", content.id);
        }

        [When(@"I send request to update user with Name '(.*)'and Job '(.*)'")]
        public async Task WhenISendRequestToUpdateUserWithNameAndJob(string name, string job)
        {
            createUser.name = name;
            createUser.job = job;
            string id = scenarioContext.Get<string>("UserId");
            response = await api.UpdateUser<CreateUserReq>(createUser, id);
        }
        
        [Then(@"I validate user is updated")]
        public void ThenIValidateUserIsUpdated()
        {
            statusCode = response.get_StatusCode();
            var code = (int)statusCode;
            Assert.AreEqual(200, code);
            var content = HandleContent.GetContent<UpdateUserRes>(response);
            Assert.AreEqual(createUser.name, content.name);
        }
    }
}
