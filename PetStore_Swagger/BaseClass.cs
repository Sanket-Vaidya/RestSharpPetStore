using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreRestAPITests
{
    public class BaseClass
    {
        public ExtentReports extentReport;
        public ExtentTest test;
        [OneTimeSetUp]
        public void CreateReports()
        {
            string workingDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + $"\\{TestContext.CurrentContext.Test.MethodName}_Report.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extentReport = new ExtentReports();
            extentReport.AttachReporter(htmlReporter);
            extentReport.AddSystemInfo("Host Name", "Local Host");
            extentReport.AddSystemInfo("Environment", "QA");
            extentReport.AddSystemInfo("Username", "Sanket.Vaidya");
        }
        [SetUp]
        public void AddTest()
        {
            test = extentReport.CreateTest(TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public void TearDown()
        {
            var result = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;


            if (result == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test failed");
                test.Log(Status.Fail, "Test failed with logtrace " + stackTrace);
            }
            else if (result == NUnit.Framework.Interfaces.TestStatus.Passed)
            {

            }
            extentReport.Flush();
        }
    }
}
