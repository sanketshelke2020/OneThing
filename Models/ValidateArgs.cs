using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.VisualBasic;
using OpenQA.Selenium.DevTools.V127.LayerTree;
using OpenQA.Selenium.Internal;

namespace OneThing.Models
{
    public static class ValidateArgs
    {
        public static void IsArgsValid(string[] args, AppArgs appArgs)
        {

            if (args.Length == 0 || Array.Exists(args, arg => arg == "--help" || arg == "-h"))
            {
                Helper.DisplayHelp(appArgs.TaskList);
                Environment.Exit(0);
            }
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(option =>
                {
                    if (IsValidTask(appArgs.TaskList, option.TaskNumber))
                    {
                        var task = appArgs.TaskList.FirstOrDefault(task => task.Id == option.TaskNumber);
                        Console.WriteLine($"Selected Task: {task}");
                        appArgs.Task = task.Description;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Task : {option.TaskNumber}");
                        Environment.Exit(0);
                    }
                    if (ValidateArgs.IsValidPin(option.Pin))
                    {
                        Console.WriteLine($"MPin = {option.Pin}");
                        appArgs.Pin = option.Pin;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Pin : {option.Pin}");
                        Environment.Exit(0);
                    }
                    if (ValidateArgs.IsValidWeeks(option.Weeks))
                    {
                        Console.WriteLine($"Week = {option.Weeks}");
                        appArgs.weeks = option.Weeks;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Weeks : {option.Weeks}");
                        Environment.Exit(0);
                    }
                });
        }

        private static bool IsValidTask(List<Task> taskList, int TaskNumber)
        {
            var task = taskList.Find(t => t.Id == TaskNumber);
            return task != null;
        }

        public static bool IsValidPin(long pin)
        {
            return pin >= 100000 && pin <= 999999;
        }

        public static bool IsValidWeeks(string weeksInput)
        {
            var weeks = weeksInput.Split(',')
                                  .Select(w => w.Trim());
            foreach (var week in weeks)
            {
                if (!int.TryParse(week, out int weekNumber) || weekNumber < 0 || weekNumber > 4)
                {
                    return false;
                }
            }
            return true;
        }

    }
}