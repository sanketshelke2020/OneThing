using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using CommandLine;

namespace OneThing.Models
{
    public static class ValidateArgs
    {
        public static void IsArgsValid(string[] args,AppArgs appArgs)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(option =>
                {
                    if (ValidateArgs.IsValidPin(option.Pin))
                    {
                        Console.WriteLine($"MPin = {option.Pin}");
                        appArgs.Pin = option.Pin;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid Pin : {option.Pin}");
                    }
                });
        }

        public static bool IsValidPin(long pin){
            return pin >= 100000 && pin <= 999999;
        }
    }
}