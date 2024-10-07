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
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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
        By myAccountBtn = By.XPath("//a[@data-testid='header-adaptive-my-account-icon-container-link']");
        By emailIdInput = By.XPath("//input[@id='EmailOrAccountNumber']");
        By passwordInput = By.XPath("//input[@id='Password']");
        By signInBtn = By.XPath("//input[@name='SignInNow']");
        By acceptCookieBtn = By.XPath("//button[@id='onetrust-accept-btn-handler']");
        public static IConfiguration Configuration { get; private set; }
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
        public void Login()   
        {           
            ClickOnElement(acceptCookieBtn);            
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("configsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            string email = Configuration["Credentials:Email"];
            string password = Configuration["Credentials:Password"];
           
            ClickOnElement(myAccountBtn);
            SendText(emailIdInput, email);
            SendText(passwordInput, password);
            ClickOnElement(signInBtn);

        }


    }
}
