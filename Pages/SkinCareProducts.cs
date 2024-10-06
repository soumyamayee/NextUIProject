using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        By listOfAllProducts = By.XPath("//p[@data-testid='product_summary_title']");
        By moreOption = By.XPath("//span[@data-testid='plp-horizontal-filter-more-less-icon']");
        By sortFilter = By.Id("desktop-sort-select");
        public void ClickOnFilter(string catagory)
        {
            try
            {
                if (catagory.Equals("More"))
                {
                    JSScrollToElement(moreOption);
                    ClickOnElement(moreOption);
                }
                else {
                    By horizontalMainFilter = By.XPath("//button[@data-testid='plp-filter-label-button-" + catagory + "']");
                    JSScrollToElement(horizontalMainFilter);
                    ClickOnElement(horizontalMainFilter);
                }
               
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
            WaitFor();
            IList<IWebElement> allElements = driver.FindElements(listOfAllProducts);
            Boolean foundMisMatch = false;
            
            for (int i = 0; i < driver.FindElements(listOfAllProducts).Count; i++)
            {
                WaitFor();
                IWebElement ele = driver.FindElements(listOfAllProducts)[i];
                String title = ele.GetAttribute("title");                
               
                if ((!title.Contains(type)) && (!title.Contains("Skin")) && (!title.Contains("Face")) && (!title.Contains("Clarins")))
                {
                    foundMisMatch = true;
                    Console.WriteLine("Displayed product is not from the Expected Brand " + type + " Rather it's from " + title + " Brand.");
                }
            }
            if (!foundMisMatch)
            {
                return true;
            }
            else {
                return false;
            }
           
            
        }
        public void SelectSortByList(String sortByValue)
        {
            //SelectElement dropDown =new SelectElement(FindElement(sortFilter));
            //dropDown.SelectByValue(sortByValue);
            By eleSortByCategory = By.XPath("//ul[@role='listbox']//span[text()='"+ sortByValue + "']");
            ClickOnElement(sortFilter);
            ClickOnElement(eleSortByCategory);

        }
        public Boolean VerifyPriceSort(String sortByValue) {
            WaitFor();
            IList <IWebElement> allProducts = driver.FindElements(listOfAllProducts);
            Console.WriteLine("All products count is " + allProducts.Count);
            IList <double> priceList= new List<double>();
           
            foreach (IWebElement eleProduct in allProducts) {
               
                WaitFor();
                String text = eleProduct.GetAttribute("title");
                Console.WriteLine("Product Title is " + text);
                 text = eleProduct.GetAttribute("title").Split("|")[1].Replace("£","").Trim();
                Console.WriteLine("The Price value is "+ text);
                double price = double.Parse(text);
                priceList.Add(price);             

            }
            IList<double> expectedPriceList = new List<double>(priceList);
            if (sortByValue.Contains("High to low price"))
            {
                expectedPriceList.OrderByDescending(price => price).ToList();
            }
            else
            {
                expectedPriceList.OrderBy(price => price).ToList();

            }
            
           
           

            if (priceList.SequenceEqual(expectedPriceList))
            {
                Console.WriteLine("The prices are correctly sorted in descending order.");
                return true;
            }
            else{
                Console.WriteLine("The prices are not correctly sorted in descending order.");
                return false;
            }
        }

    }
}
