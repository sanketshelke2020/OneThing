using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneThing.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }


    }
    public class TaskList
    {
        public List<Task> Tasks { get; set; }
    }

}