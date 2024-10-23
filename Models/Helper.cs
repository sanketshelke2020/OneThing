using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace OneThing.Models
{
    public static class Helper
    {
        public static void WaitForSpinnerToDisappear(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(1000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver =>
                {
                    var spinner = driver.FindElement(By.ClassName("v-spinner"));
                    return !spinner.Displayed;
                });
                Thread.Sleep(1000);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Spinner did not disappear.");
            }
        }
        public static void DoubleClickElement(IWebDriver driver, IWebElement element)
        {
            Actions actions = new Actions(driver);
            actions.DoubleClick(element).Perform();
        }
    }
}