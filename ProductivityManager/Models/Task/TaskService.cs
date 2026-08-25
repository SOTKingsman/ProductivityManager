using System.Collections.ObjectModel;

namespace ProductivityManager.Models.Task;

public class TaskService
{
    public ObservableCollection<TaskModel> Tasks { get; } = new();
    
    public void DeleteTask(TaskModel task)
    {
        if (Tasks.Contains(task))
        {
            Tasks.Remove(task);
        }
    }
}