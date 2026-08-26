using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ProductivityManager.Models.Task;
using Task = ProductivityManager.Models.Task.Task;

namespace ProductivityManager.Views.Models.TaskDatabase;

public class Database
{
    public List<Task> Tasks { get; } = new List<Task>();
 
    public bool AddTask(Task task)
    {
        if (task == null)
        {
            return false;
        }
 
        Tasks.Add(task);
        return true;
    }
 
    public bool DeleteTask(Task task)
    {
        return Tasks.Remove(task);
    }
 
    public List<Task> GetTasks()
    {
        return Tasks;
    }
    
    public class TaskData
    {
        public string TaskName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
 
    public void SaveToFile(string filePath)
    {
        List<TaskData> dataToSave = Tasks.Select(t => t.GetTaskData()).ToList();
        
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(dataToSave, options);
 
        File.WriteAllText(filePath, json);
    }
 
    public List<TaskData> LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<TaskData>();
        }
 
        string json = File.ReadAllText(filePath);
        List<TaskData> loadedData = JsonSerializer.Deserialize<List<TaskData>>(json);
 
        return loadedData ?? new List<TaskData>();
    }
}




