using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using NextUISpecFlowPOM.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NextUISpecFlowPOM.StepDefinitions
{
    [Binding]
    public class NextProductSearch
    {
        IWebDriver driver;
        HomePage homePage;
        SkinCareProducts skinCareProducts;
        [BeforeScenario]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.next.co.uk");
            homePage = new HomePage(driver);
            skinCareProducts = new SkinCareProducts(driver);
        }
        [AfterScenario]
        public void TearDown() {
            if (driver != null) { 
                driver.Quit();
            }
        }

        [Given(@"User on Next Home Page")]
        public void GivenUserOnNextHomePage()
        {
            Console.WriteLine("Title is " + driver.Title);
            Assert.Equals("https://www.next.co.uk/", driver.Title);

        }

        [When(@"User Navigated to the ""([^""]*)"" section")]
        public void WhenUserNavigatedToTheSection(string catagory)
        {
            homePage.NavigateToSection();
            
        }

        [Then(@"User select ""([^""]*)"" category")]
        public void ThenUserSelectCategory(string subCatagory)
        {
            homePage.NavigateToCatagory();
        }

        [Then(@"User Click on ""([^""]*)"" filter")]
        public void ThenUserClickOnFilter(string brand)
        {
            skinCareProducts.ClickOnFilter(brand);
        }

        [Then(@"User Click on ""([^""]*)"" brand")]
        public void ThenUserClickOnBrand(string next)
        {
            skinCareProducts.SelectBrand(next);
        }
        [Then(@"User verify only the ""([^""]*)"" brand products displayed")]
        public void ThenUserVerifyOnlyTheBrandProductsDisplayed(string type)
        {
            skinCareProducts.VerifyProductsTitle(type);
        }


    }
}
