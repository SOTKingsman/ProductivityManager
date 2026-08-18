namespace ProductivityManager.Views.Models.TaskDatabase;

public class Database
{
    private List<Task> _tasks = new List<Task>();

    public bool addTask (Task task)
    {
        if (task == null)
        {
            return false;
        }
        
        _tasks .Add(task);
        return true;
    }

    public bool deleteTask(Task task)
    {
        return _tasks.Remove(task);
    }
    
    public List<Task> GetTasks()
    {
        return _tasks;
    }
    



}