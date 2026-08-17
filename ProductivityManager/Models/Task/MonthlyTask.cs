namespace ProductivityManager.Models.Task;

public class MonthlyTask : Task
{
    public MonthlyTask(string taskName, string category, string description,DateTime start,DateTime end) : base(taskName, category, description,start,end)
    {
    }
}