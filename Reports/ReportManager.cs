
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Reflection;

namespace NextUISpecFlowPOM.Reports
{
    internal static class ReportManager
    {
        public static ExtentReports extent;
        public static ExtentTest test;
        //private static string reportPath;
        public static void InitializeReport()


        {
            string path = Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;

            string reportPath = projectPath + "Reports\\";

            System.IO.Directory.CreateDirectory(reportPath);
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Tester", Environment.UserName);
            extent.AddSystemInfo("MachineName", Environment.MachineName);

            /* string name = DateTime.Now.ToString("HHMMss");
             var htmlreporter = new ExtentHtmlReporter(@"C:\Soumya\Projects\NextUIProjectGITHUB\Reports" + name + ".html");
             //htmlreporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
             extent = new ExtentReports();
             extent.AttachReporter(htmlreporter);*/

            // Define the report path
            /*var reportDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\Reports");
            var reportPath = Path.Combine(reportDirectory, "ExtentReport.html");
            

    // Create the Reports directory if it does not exist
    if (!Directory.Exists(reportDirectory))
    {
        Directory.CreateDirectory(reportDirectory);
        Console.WriteLine($"Created directory: {reportDirectory}"); // Debugging output
    }

    // Initialize the ExtentReports instance
    var htmlReporter = new ExtentHtmlReporter(reportPath);
    extent = new ExtentReports();
    extent.AttachReporter(htmlReporter);
    Console.WriteLine($"Extent Report initialized at: {reportPath}"); // Debugging output*/
        }

        public static void CreateTest(string testName)
        {
            // Create a new test in the report
            test = extent.CreateTest(testName);
        }

        public static void LogInfo(string message)
        {
            test.Log(Status.Info, message);
        }

        public static void LogPass(string message)
        {
            test.Log(Status.Pass, message);
        }

        public static void LogFail(string message)
        {
            test.Log(Status.Fail, message);
        }

        public static void FlushReport()
        {
            Console.WriteLine("Flushing report..."); // Debugging output
            try
            {
                extent.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error flushing report: {ex.Message}"); // Log the error message
                throw; // Optionally rethrow the exception to let it propagate
            }
        }
    }
}

