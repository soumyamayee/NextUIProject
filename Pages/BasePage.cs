using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUISpecFlowPOM.Pages
{
    internal class BasePage
    {
        IWebDriver driver;
        Actions actions;
        public BasePage(IWebDriver driver) { this.driver = driver;
             actions=new Actions(driver);
        }

        // Method to find elements (can be used in child classes)
        protected IWebElement FindElement(By locator)
        {
            return driver.FindElement(locator);
        }
        public void ClickOnElement(By ele) {
            WaitForElementToBeClickable(FindElement(ele));
            FindElement(ele).Click();
        }
        public void SendText(By ele, String text)
        {
            WaitForElementToBeVisible(ele);
            FindElement(ele).SendKeys(text);
        }   
        public void MoveToElement(By ele)
        {
            actions.MoveToElement(FindElement(ele)).Perform();
        }
        public void WaitForElementToBeClickable(IWebElement ele) {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele));

        }
        public void WaitForElementToBeVisible(By locator) { 
            WebDriverWait wait =new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }
        public void JSScrollToElement(By locator) {
            WaitForElementToBeVisible(locator);
           ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(locator));           
        }
        public void JSClickOnElement(By locator)
        {
            
            Console.WriteLine("locator is "+ locator);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(locator));
        }
        protected IList<IWebElement> FindAllElements(By locator) { 
           return driver.FindElements(locator);
        }
    }
}
