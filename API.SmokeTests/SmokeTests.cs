using API.Automation;
using API.Automation.Models;
using API.Automation.Models.Request;
using API.Automation.Models.Response;
using API.Automation.Utilities;
using API.SmokeTests.Hook;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace API.SmokeTests
{
    [TestClass]
    public class SmokeTests
    {
        public TestContext TestContext { get; set; }
        public HttpStatusCode statusCode;

        [ClassInitialize]
        public static void SetUpReport(TestContext testContext)
        {
            var dir = testContext.TestRunDirectory;
            Reporter.SetUpReport(dir, "SmokeTest", "Smoke test result");
        }

        [TestInitialize]
        public void SetUpTest()
        {
            Reporter.CreateTest(TestContext.TestName);
        }

        [TestCleanup]
        public void TearDownTest()
        {
            var testStatus = TestContext.CurrentTestOutcome;
            Status status;

            switch (testStatus)
            {
                case UnitTestOutcome.Failed:
                    status = Status.Fail;
                    Reporter.TestStatus(status.ToString());
                    break;
                case UnitTestOutcome.Inconclusive:
                    break;
                case UnitTestOutcome.Passed:
                    status = Status.Pass;
                    break;
                case UnitTestOutcome.InProgress:
                    break;
                case UnitTestOutcome.Error:
                    break;
                case UnitTestOutcome.Timeout:
                    break;
                case UnitTestOutcome.Aborted:
                    break;
                case UnitTestOutcome.Unknown:
                    break;
                case UnitTestOutcome.NotRunnable:
                    break;
                default:
                    break;
            }
        }

        [ClassCleanup]
        public static void CleanUp()
        {
            Reporter.FlushReport();
        }

        [TestMethod]
        public async Task get_list_of_users()
        {
            var api = new APIClient(Hooks.config.Token);
            var response = await api.GetUser(2);
            statusCode = response.get_StatusCode();
            var code = (int)statusCode;
            Assert.AreEqual(200, code);

            var content = HandleContent.GetContent<Users>(response);
            Assert.AreEqual(2, content.page);
        }

        [TestMethod]
        public async Task create_users()
        {
            var payload = new CreateUserReq
            {
                name = "Mike",
                job = "Dev"
            };
            var api = new APIClient(Hooks.config.Token);
            var response = await api.CreateUser<CreateUserReq>(payload);
            statusCode = response.get_StatusCode();
            var code = (int)statusCode;
            Assert.AreEqual(201, code);

            var content = HandleContent.GetContent<CreateUserRes>(response);
            Assert.AreEqual(payload.name, content.name);
        }
    }
}