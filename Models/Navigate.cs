using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace OneThing.Models
{
    public static class Navigate
    {
        public static void SwitchWorkSpace(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div/header/div/div[2]/div[1]/div/span")).Click();
            Helper.WaitForSpinnerToDisappear(driver);
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div[1]/div[2]/div/div[1]/div[2]/div[2]/div/div/div[1]/div/span")).Click();
            Helper.WaitForSpinnerToDisappear(driver);
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div[1]/div[2]/span")).Click();
        }

        public static void WeeklySchedule(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div[2]/div[2]/nav/ul/li[4]/a")).Click();
            Helper.WaitForSpinnerToDisappear(driver);
            driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div[2]/div[1]/div/div[1]/div[2]")).Click();
            Helper.WaitForSpinnerToDisappear(driver);
        }
    }
}