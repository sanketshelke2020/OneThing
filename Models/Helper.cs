using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
                Thread.Sleep(2000);
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

        public static void FillDailyLogForm(IWebDriver driver,string task)
        {
            string InTime = "09:30 AM";
            string OutTime = "06:30 PM";

            var SelectTask = new SelectElement(driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[2]/div[2]/select")));
            SelectTask.SelectByText(task);
            var InTimeElement = new SelectElement(driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[4]/div[2]/select[1]")));
            InTimeElement.SelectByText(InTime);
            var OutTimeElement = new SelectElement(driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[4]/div[2]/select[5]")));
            OutTimeElement.SelectByText(OutTime);
            driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[2]")).Click();

            WaitForSpinnerToDisappear(driver);
        }

        public static List<Task> LoadTasks(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var taskList = JsonSerializer.Deserialize<TaskList>(json);
            return taskList.Tasks;
        }

        public static void DisplayHelp(List<Task> tasks)
        {
            Console.WriteLine("Extended Help:");
            Console.WriteLine("This application allows you to perform various tasks.");
            Console.WriteLine("Usage:");
            Console.WriteLine("  yourapp.exe -p <pin> -t <task_number> -w <week behind : 1 last week , 0 current week (0-4)>");
            Console.WriteLine();
            Console.WriteLine("Available tasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"  {task.Id}: {task.Description}");
            }
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  yourapp.exe -p 1234 -t 1 -w 1,2  # Executes Task One");
            Console.WriteLine("  yourapp.exe --help          # Displays this help");
        }
        
    }
}