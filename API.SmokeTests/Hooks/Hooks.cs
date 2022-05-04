using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace API.SmokeTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static TestContext TestContext { get; set; }
        private ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun(TestContext testContext)
        {
            var dir = testContext.TestRunDirectory;
            htmlReporter = new ExtentHtmlReporter(dir);
            htmlReporter.Config.Theme = Theme.Dark;
            //htmlReporter.Config.DocumentTitle = documentTitle;
            //htmlReporter.Config.ReportName = reportName;

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            Console.WriteLine("BeforeFeature");
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine("BeforeScenario");
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public static void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            var stepContext = scenarioContext.StepContext;
            var stepType = stepContext.StepInfo.StepDefinitionType.ToString();

            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(stepContext.StepInfo.Text);
                else if(stepType == "When")
                                scenario.CreateNode<When>(stepContext.StepInfo.Text);
                else if(stepType == "Then")
                                scenario.CreateNode<Then>(stepContext.StepInfo.Text);
                else if(stepType == "And")
                                scenario.CreateNode<And>(stepContext.StepInfo.Text);
            }
            else if(scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(stepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
                else if(stepType == "When")
                {
                    scenario.CreateNode<When>(stepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
                else if(stepType == "Then") {
                    scenario.CreateNode<Then>(stepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
                else if(stepType == "And")
                {
                    scenario.CreateNode<And>(stepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message);
                }
            }
        }
        [AfterScenario]
        public static void AfterScenario()
        {
            Console.WriteLine("AfterScenario");
        }
        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }
    }
}
