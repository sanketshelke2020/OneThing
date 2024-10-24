using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneThing.Models
{
    public class AppArgs
    {
        public long Pin { get; set; }
        public string? Task { get; set; }
        public List<Task> TaskList { get; set; }
    }
}