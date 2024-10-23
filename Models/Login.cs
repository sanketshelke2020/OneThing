using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OneThing.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string URL { get; set; }
        public string Domain { get; set; }
        public string Pin { get; set; }
        public Login(string email, string uRL, string domain,string pin)
        {
            Email = email;
            URL = uRL;
            Domain = domain;
            Pin = pin;
        }

        public void LoginToOneThing(IWebDriver driver){
            driver.Navigate().GoToUrl(URL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);  

            driver.FindElement(By.Id("login_email")).SendKeys(Email);
            var domainElement = new SelectElement(driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div[2]/div/div/div/div/form/div[1]/div/select")));
            domainElement.SelectByValue(Domain);

            var pinElements = driver.FindElements(By.ClassName("otp-input"));
            for (int i = 0; i < pinElements.Count && i < Pin.Length; i++)
            {
                pinElements[i].SendKeys(Pin[i].ToString());
            }

            driver.FindElement(By.Id("btn_do_login")).Click();
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);  
            Helper.WaitForSpinnerToDisappear(driver);
            Thread.Sleep(5000);
            System.Console.WriteLine(driver.Url);
            if(!IsLogedIn(driver)) {
                Console.WriteLine("Login Failed!");
                driver.Close();
                Environment.Exit(0);
            }
        }

        public bool IsLogedIn(IWebDriver driver){
            string homePageURL = "home";
            return driver.Url.Contains(homePageURL);
        }
 

    }
}