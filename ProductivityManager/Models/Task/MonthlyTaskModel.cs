namespace ProductivityManager.Models.Task;

public class MonthlyTaskModel : TaskModel
{
    public MonthlyTaskModel(string taskName, string category, string description,DateTime start,DateTime end) : base(taskName, category, description,start,end)
    {
    }
}