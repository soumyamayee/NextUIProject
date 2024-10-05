using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V129.Network;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUISpecFlowPOM.Pages
{
    internal class HomePage:BasePage
    {
        IWebDriver driver;
        public HomePage(IWebDriver driver) :base(driver){
            this.driver = driver;
        }
        By womenSection = By.XPath("//div[text()='women']");
        By skinCareCatagory = By.XPath("//span[text()='Skincare']");
        public void NavigateToSection() {
            ClickOnElement(womenSection);
        }
        public void NavigateToCatagory()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
             wait.Until(ExpectedConditions.ElementIsVisible(skinCareCatagory));            
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(skinCareCatagory));
            ClickOnElement(skinCareCatagory);
        }

    }
}
