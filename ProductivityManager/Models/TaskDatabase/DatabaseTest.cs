using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProductivityManager.Models.Task;
using ProductivityManager.Views.Models.TaskDatabase;

namespace ProductivityManager.Models.Task;

public static class DatabaseTests
{
    public static void RunTest()
    {
        Database database = new Database();

        database.AddTask(new TestTask(
            "Finish report",
            "Work",
            "Write the quarterly summary",
            new DateTime(2026, 8, 26, 9, 0, 0),
            new DateTime(2026, 8, 26, 11, 0, 0)
        ));

        database.AddTask(new TestTask(
            "Gym session",
            "Health",
            "Leg day",
            new DateTime(2026, 8, 26, 18, 0, 0),
            new DateTime(2026, 8, 26, 19, 0, 0)
        ));

        database.SaveToFile("tasks.json");

        List<Database.TaskData> loaded = database.LoadFromFile("tasks.json");

        foreach (var task in loaded)
        {
            Debug.WriteLine($"{task.TaskName} ({task.Category}): {task.StartDateTime} - {task.EndDateTime}");
        }
    }
}