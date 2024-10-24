using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;

namespace OneThing.Models
{
    public class Options
    {
        [Option('p', "pin", Required = true, HelpText = "Please provide MPin from CONNECTO")]
        public long Pin { get; set; }
        [Option('t', "task", Required = true, HelpText = "Please provide the task number.\n")]
        public int TaskNumber { get; set; }
       
    }
}

