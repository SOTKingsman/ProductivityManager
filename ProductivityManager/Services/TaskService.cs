using System.Collections.ObjectModel;
using System.IO;
using ProductivityManager.Views.Models.TaskDatabase;

namespace ProductivityManager.Models.Task;

public class TaskService
{

    private static readonly string DefaultFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ProductivityManager",
        "tasks.json"
    );

    private readonly string _filePath;

    public ObservableCollection<TaskModel> Tasks { get; } = new();

    public TaskService(string? filePath = null)
    {
        _filePath = filePath ?? DefaultFilePath;

        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        foreach (TaskModel task in Database.LoadFromFile(_filePath))
        {
            Tasks.Add(task);
        }

        Tasks.CollectionChanged += (_, _) => Save();
    }

    public void AddTask(TaskModel task)
    {
        Tasks.Add(task);
    }

    public void UpdateTask(TaskModel oldTask, TaskModel newTask)
    {
        int index = Tasks.IndexOf(oldTask);

        if (index >= 0)
        {
            Tasks[index] = newTask;
        }
    }

    public void DeleteTask(TaskModel task)
    {
        if (Tasks.Contains(task))
        {
            Tasks.Remove(task);
        }
    }

    public void Save()
    {
        Database.SaveToFile(Tasks, _filePath);
    }
}