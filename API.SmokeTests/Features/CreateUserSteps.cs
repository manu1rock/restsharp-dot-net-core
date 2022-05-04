using API.Automation;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utilities;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace API.SmokeTests.Features
{
    [Binding]
    public class CreateUserSteps
    {
        private readonly CreateUserReq createUser;
        private RestResponse response;
        private HttpStatusCode statusCode;
        private ISpecFlowOutputHelper _specFlowOutputHelper;
        public CreateUserSteps(CreateUserReq createUser, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this.createUser = createUser;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [Given(@"I input name ""(.*)""")]
        public void GivenIInputName(string name)
        {
            createUser.name = name;
            _specFlowOutputHelper.WriteLine("");
        }
        
        [Given(@"I input role ""(.*)""")]
        public void GivenIInputRole(string role)
        {
            createUser.job = role;
        }
        
        [When(@"I send create user request")]
        public async Task WhenISendCreateUserRequest()
        {
            var api = new APIClient();
            response = await api.CreateUser<CreateUserReq>(createUser);
            statusCode = response.get_StatusCode();
            var code = (int)statusCode;
            Assert.AreEqual(201, code);
        }
        
        [Then(@"validate user is created")]
        public void ThenValidateUserIsCreated()
        {
            var content = HandleContent.GetContent<CreateUserRes>(response);
            Assert.AreEqual(createUser.name, content.name);
            Assert.AreEqual(createUser.job, content.job);
        }
    }
}
