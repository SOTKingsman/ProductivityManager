using System;
using System.Diagnostics;
using ProductivityManager.Views.Models.TaskDatabase;

namespace ProductivityManager.Models.Task;


public class TestTask : Task
{
    public TestTask(string name, string category, string description, DateTime start, DateTime end)
        : base(name, category, description, start, end)
    {
    }

}