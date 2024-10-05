using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUISpecFlowPOM.Pages
{
    internal class SkinCareProducts : BasePage
    {
        IWebDriver driver;
        public SkinCareProducts(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }
        static String text = "";

        By genderWomen = By.XPath("//div[@data-testid='horizontal-filter-facet-Women']//input[@type='checkbox']");
        By listOfAllProducts = By.XPath("//div[contains(@data-testid,'product_tile_card_content_')]//p[@data-testid='product_summary_title']");

        public void ClickOnFilter(string catagory)
        {
            try
            {
                By horizontalMainFilter = By.XPath("//button[@data-testid='plp-filter-label-button-" + catagory + "']");
                JSScrollToElement(horizontalMainFilter);
                ClickOnElement(horizontalMainFilter);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        public void SelectBrand(string brandName)
        {
            try
            {
                By subFilterCheckBox = By.XPath("//div[@data-testid='horizontal-filter-facet-" + brandName + "']//input[@type='checkbox']");
                JSClickOnElement(subFilterCheckBox);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }
        public Boolean VerifyProductsTitle(String type)
        {
            if (type.Equals("next", StringComparison.OrdinalIgnoreCase))
            {
                type = "Beauty Box";
            }
            IList<IWebElement> allElements = driver.FindElements(listOfAllProducts);
            Boolean foundMisMatch = false;
            
            for (int i = 0; i < driver.FindElements(listOfAllProducts).Count; i++)
            {
                IWebElement ele = driver.FindElements(listOfAllProducts)[i];
                String title = ele.GetAttribute("title");                
                Thread.Sleep(1000);
                if ((!title.Contains(type)) && (!title.Contains("Skin")) && (!title.Contains("Face")) && (!title.Contains("Clarins")))
                {
                    foundMisMatch = true;
                    Console.WriteLine("Displayed product is not from the Expected Brand " + type + " Rather it's from " + title + " Brand.");
                }
            }
            if (foundMisMatch)
            {
                return false;
            }
            else {
                return true;
            }
           
            
        }

    }
}
