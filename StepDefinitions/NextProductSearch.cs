using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using NextUISpecFlowPOM.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NextUISpecFlowPOM.Reports;

namespace NextUISpecFlowPOM.StepDefinitions
{
    [Binding]
    public class NextProductSearch
    {
        IWebDriver driver;
        HomePage homePage;
        SkinCareProducts skinCareProducts;
        
        // Setup report
        

        [BeforeScenario]
        public void Setup()
        {
            ReportManager.InitializeReport();
            ReportManager.CreateTest("My First Test");
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.next.co.uk");
            homePage = new HomePage(driver);
            skinCareProducts = new SkinCareProducts(driver);
            ReportManager.LogPass("Navigated to Next Home Page");
        }
        [AfterScenario]
        public void TearDown()
        {
            if (driver != null)
            {
                
                driver.Quit();
            }
        }

        [Given(@"User on Next Home Page")]
        public void GivenUserOnNextHomePage()
        {
            Console.WriteLine("Title is " + driver.Title);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Next Official Site: Online Fashion, Kids Clothes & Homeware", driver.Title);

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
            Boolean statusFlag=skinCareProducts.VerifyProductsTitle(type);
            if (statusFlag)
            {
                NUnit.Framework.Assert.Pass("All displayed products are from the expected brand: " + type);
            }
            else {
                NUnit.Framework.Assert.Fail("All displayed products are not from the expected brand: " + type);
            }

        }
        [Then(@"User select Sort ""([^""]*)"" filter")]
        public void ThenUserSelectSortFilter(string sortValue)
        {
            skinCareProducts.SelectSortByList(sortValue);
        }

        [Then(@"User verify the products are displayed from ""([^""]*)""")]
        public void ThenUserVerifyTheProductsAreDisplayedFrom(string sortValue)
        {
            Boolean statusFlag = skinCareProducts.VerifyPriceSort(sortValue);
            if (statusFlag) {
                NUnit.Framework.Assert.Pass("The prices are correctly sorted in descending order.");
            }
            else
            {
                NUnit.Framework.Assert.Fail("The prices are correctly sorted in descending order. ");
            }
        }
        [Then(@"User Select the Price range from ""([^""]*)""")]
        public void ThenUserSelectThePriceRangeFrom(string priceRange)
        {
            skinCareProducts.ApplyPriceFilter(priceRange);
        }

        [Then(@"User verify the products displayed are between the price range ""([^""]*)""")]
        public void ThenUserVerifyTheProductsDisplayedAreBetweenThePriceRange(string priceRange)
        {
           
        }
        [Then(@"User login to Next shopping site")]
        public void ThenUserLoginToNextShoppingSite()
        {
            homePage.Login();
        }



    }
}
