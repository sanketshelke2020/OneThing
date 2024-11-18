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
appArgs.TaskList = Helper.LoadTasks("./tasks.json");
ValidateArgs.IsArgsValid(args, appArgs);

IWebDriver driver = new ChromeDriver();
Login login = new Login(AppSettings.Email, AppSettings.URL, AppSettings.Domain, appArgs.Pin.ToString());
login.LoginToOneThing(driver);
driver.Manage().Window.Maximize();

Navigate.SwitchWorkSpace(driver);
Navigate.WeeklySchedule(driver);


var weekOffsets = appArgs.weeks.Split(',')
                       .Select(w => int.TryParse(w.Trim(), out var week) ? week : -1)
                       .Where(w => w >= 0 && w <= 4)
                       .Distinct()
                       .OrderBy(w => w) // Sort in ascending order
                       .ToList();

int currentWeek = 0;
foreach (var weekOffset in weekOffsets)
{
    int diff = weekOffset - currentWeek;

    if (diff > 0)
    {
        for (int clicks = 0; clicks < diff; clicks++)
        {
            var prevWeekButton = driver.FindElement(By.ClassName("dhx_cal_prev_button"));
            prevWeekButton.Click();
        }
    }
    var weekDays = driver.FindElements(By.XPath("//*[contains(@class, 'dhx_scale_holder')]"));

    int totalDays = weekDays.Count;

    for (int i = 0; i < totalDays - 3; i++)
    {
        var day = weekDays[i];
        Helper.DoubleClickElement(driver, day);
        Helper.FillDailyLogForm(driver, appArgs.Task);
    }
    currentWeek = weekOffset;
}

Console.WriteLine("Press Enter to exit...");
Console.ReadLine();
driver.Close();