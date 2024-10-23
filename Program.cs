using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using OneThing.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
            .Build();
AppSettings AppSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

AppArgs appArgs = new AppArgs();
ValidateArgs.IsArgsValid(args, appArgs);

IWebDriver driver = new ChromeDriver();
Login login = new Login(AppSettings.Email, AppSettings.URL ,AppSettings.Domain,appArgs.Pin.ToString());
login.LoginToOneThing(driver);
driver.Manage().Window.Maximize();

Navigate.SwitchWorkSpace(driver);
Navigate.WeeklySchedule(driver);

var weekDays = driver.FindElements(By.XPath("//*[contains(@class, 'dhx_scale_holder')]"));
// var monday = driver.FindElement(By.XPath("//*[@id=\"app\"]/div/div[2]/div[1]/div/div[5]/div[1]"));
foreach(var day in weekDays){

}
// Helper.DoubleClickElement(driver, monday);


Console.WriteLine("Press Enter to exit...");
Console.ReadLine();
driver.Close();