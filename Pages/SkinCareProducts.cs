using NUnit.Framework;
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
        By minPriceSlider = By.XPath("//input[@aria-label='Set minimum value']");
        By maxPriceSlider = By.XPath("//input[@aria-label='Set maximum value']");
        public void ClickOnFilter(string catagory)
        {
            try
            {
                if (catagory.Equals("More"))
                {
                    //JSScrollToElement(moreOption);
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


        public void SetPriceRange(String price) {
            String minPrice=price.Split("-")[0].Replace("£", "").Trim();
            String maxPrice = price.Split("-")[1].Replace("£", "").Trim();
            Console.WriteLine("Min price to set is " + minPrice);
            Console.WriteLine("Max price to set is " + maxPrice);
            //JSScrollToElement(minPriceSlider);

            WaitFor();

            // JavaScript to set the slider value without UI updates
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            // Set min price
            js.ExecuteScript("arguments[0].value='" + minPrice + "';", FindElement(minPriceSlider));

            // Set max price
            js.ExecuteScript("arguments[0].value='" + maxPrice + "';", FindElement(maxPriceSlider));

            // Now simulate a user interaction to update the UI
           
            /*
            SendText(minPriceSlider, minPrice);
            WaitFor();
            SendText(maxPriceSlider, maxPrice);*/
        }

        public bool ApplyPriceFilter(String price)
        {
            try
            {
                String lowPrice = price.Split("-")[0].Replace("£", "").Trim();
                String highPrice = price.Split("-")[1].Replace("£", "").Trim();
                Console.WriteLine("Min price to set is " + lowPrice);
                Console.WriteLine("Max price to set is " + highPrice);
                // Remove all non-numeric characters from the high price and convert to integer
               // string temp = System.Text.RegularExpressions.Regex.Replace(highPrice, "[^0-9]", "");
                int maxValue = int.Parse(highPrice);

                // Remove all non-numeric characters from the low price and convert to integer
                //temp = System.Text.RegularExpressions.Regex.Replace(lowPrice, "[^0-9]", "");
                int minPrice = int.Parse(lowPrice);
                setUpperSliderToTargetValue(maxValue);

                return true;  // Return true if successful
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);  // Log the error message to the console
                throw new Exception("Failed to apply price filter", e);  // Re-throw the exception with a custom message
            }
        }


        public void setUpperSliderToTargetValue(int targetVal)
        {
            try
            {
                //WaitForElementToBeVisible(minPriceSlider);

                IWebElement minPriceElement = FindElement(minPriceSlider);
                IWebElement maxPriceElement = FindElement(maxPriceSlider);
                // Get current aria-valuetext values for both sliders
                decimal upperValue = getSliderValue(maxPriceElement);  // Aria-valuetext of the upper slider
                decimal lowerValue = getSliderValue(minPriceElement);   // Aria-valuetext of the lower slider

                Console.WriteLine("Current Upper Slider Value: " + upperValue);
                Console.WriteLine("Current Lower Slider Value: " + lowerValue);

                // Parse the target value
                decimal targetValue = targetVal;
                Console.WriteLine("Target Value: " + targetValue);

                // Validate that the target value is within the slider range
                if (targetValue <= lowerValue || targetValue > upperValue)
                {
                    throw new ArgumentException("Target value must be greater than lower slider value and less than or equal to the upper slider value.");
                }

                // Initialize JavaScriptExecutor
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                Thread.Sleep(100);
                // Move the slider one step at a time
                String previousValueText = maxPriceElement.GetAttribute("aria-valuetext");
                String currentValueText = previousValueText;

                int step = 20;  // Initial step size

                while (true)
                {
                    string temp = System.Text.RegularExpressions.Regex.Replace(currentValueText, "[^0-9]", "");
                    int currValue = int.Parse(temp);
                    Console.WriteLine("Current Reached Value is: " + currValue);
                    if (currValue <= (20 + targetValue))
                    {
                        Console.WriteLine("currValue <= (20 + targetValue)");
                        //Slider Value close enough use smaller steps 
                        step = 1;
                    }
                    if (currValue > targetValue)
                    {
                        Console.WriteLine("INside currValue > targetValue");
                        WaitFor();
                        // Move the slider by a small step to the left (negative x direction)
                        //js.ExecuteScript("arguments[0].value -= arguments[1]; arguments[0].dispatchEvent(new Event('change'));",FindElement(maxPriceSlider), step);
                        js.ExecuteScript(
                            "arguments[0].value = arguments[1]; " +
                            "arguments[0].dispatchEvent(new Event('input')); " +  // Trigger input event
                            "arguments[0].dispatchEvent(new Event('change')); " + // Trigger change event
                            "arguments[0].dispatchEvent(new MouseEvent('mousedown', { bubbles: true }));" + // Simulate mousedown event
                            "arguments[0].dispatchEvent(new MouseEvent('mouseup', { bubbles: true }));",  // Simulate mouseup event
                            FindElement(maxPriceSlider), 100
                            );
                        //js.ExecuteScript("arguments[0].value -= arguments[1]; arguments[0].dispatchEvent(new Event('change'));", FindElement(maxPriceSlider), 100);



                    }
                    else
                    {
                        break;
                    }
                    // Fetch the new aria-valuetext after moving the slider
                    currentValueText = maxPriceElement.GetAttribute("aria-valuetext");
                    Console.WriteLine("Current aria-valuetext: " + currentValueText);

                    // Check if the slider value stopped changing
                    if (currentValueText.Equals(previousValueText))
                    {
                        Console.WriteLine("Slider stopped changing. Breaking the loop.");
                        break;
                    }

                    previousValueText = currentValueText;

                    // Exit loop if the target value is reached or very close
                    if (Math.Abs(currValue - targetValue) <= step)
                    {
                        break;
                    }
                }
                Console.WriteLine("Slider set to target value or closest possible.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Failed to set slider to target value", e);
            }
        }


        private Decimal getSliderValue(IWebElement slider)
        {
            // Get the aria-valuetext of the slider and convert it to BigDecimal
            String ariaValueText = slider.GetAttribute("aria-valuetext");
            String numericString = System.Text.RegularExpressions.Regex.Replace(ariaValueText, "[^0-9]", "");
            return decimal.Parse(numericString);
        }
    }


}
