using System.Collections.ObjectModel;

namespace ProductivityManager.Models.Task;

public class TaskService
{
    public ObservableCollection<TaskModel> Tasks { get; } = new();

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
}