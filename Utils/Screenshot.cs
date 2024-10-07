using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace NextUISpecFlowPOM.Utils
{
    internal class Screenshot
    {

        private IWebDriver _driver;

        public Screenshot(IWebDriver driver)
        {
            _driver = driver;
        }

        // Capture screenshot
        public string CaptureScreenshot(string testName)
        {
            try
            {
                // Create directory if not exists
                string screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                if (!Directory.Exists(screenshotsDir))
                {
                    Directory.CreateDirectory(screenshotsDir);
                }

                // Take screenshot
                ITakesScreenshot ts = (ITakesScreenshot)_driver;
                 ts.GetScreenshot();

                // Define screenshot file path
                string screenshotPath = Path.Combine(screenshotsDir, testName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");

                // Save screenshot
                //ts.GetScreenshot().SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                return screenshotPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error capturing screenshot: " + ex.Message);
                return null;
            }
        }
    }
}
