using API.SmokeTests.Variables;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace API.SmokeTests.Hook
{
    [Binding]
    public class Hooks
    {
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static TestContext TestContext { get; set; }
        private static ScenarioContext _scenarioContext;
        public static ConfigSetting config;
        static string configPath = Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "Configuration\\configsetting.json";

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public static void LogToReport(Status status, string message)
        {
            scenario.CreateNode(status.ToString(), message);
        }

        [BeforeTestRun]
        public static void BeforeTestRun(TestContext testContext)
        {
            config = new ConfigSetting();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(configPath);
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(config);

            var dir = testContext.TestRunDirectory;
            htmlReporter = new ExtentHtmlReporter(dir);
            htmlReporter.Config.Theme = Theme.Dark;
            //htmlReporter.Config.DocumentTitle = documentTitle;
            //htmlReporter.Config.ReportName = reportName;

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.File(dir + @"\Logs",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {Level:u3} | {Message} {NewLine}",
                rollingInterval: RollingInterval.Day).CreateLogger();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            Log.Information("Feature {0} to run", featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            
        }

        [BeforeScenario]
        public static void BeforeScenario(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            Log.Information("Scenario {0} to run", scenarioContext.ScenarioInfo.Title);
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
                Log.Error("Test step failed | " + stepContext.TestError.Message);
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
