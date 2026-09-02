using System.IO;
using System.Text.Json;
using ProductivityManager.Models.Task;

namespace ProductivityManager.Views.Models.TaskDatabase;


public static class Database
{
    public class TaskData
    {
        public string TaskName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    private static TaskData ToTaskData(TaskModel task)
    {
        return new TaskData
        {
            TaskName = task.TaskName,
            Category = task.Category,
            Description = task.Description,
            StartDateTime = task.StartDateTime,
            EndDateTime = task.EndDateTime
        };
    }

    private static TaskModel FromTaskData(TaskData data)
    {
        return new TaskModel(
            data.TaskName,
            data.Category,
            data.Description,
            data.StartDateTime,
            data.EndDateTime
        );
    }

    public static void SaveToFile(IEnumerable<TaskModel> tasks, string filePath)
    {
        List<TaskData> dataToSave = tasks.Select(ToTaskData).ToList();

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(dataToSave, options);

        File.WriteAllText(filePath, json);
    }

    public static List<TaskModel> LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<TaskModel>();
        }

        string json = File.ReadAllText(filePath);
        List<TaskData> loadedData = JsonSerializer.Deserialize<List<TaskData>>(json);

        if (loadedData == null)
        {
            return new List<TaskModel>();
        }

        return loadedData.Select(FromTaskData).ToList();
    }
}