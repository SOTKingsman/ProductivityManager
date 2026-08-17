namespace ProductivityManager.Models.Task;

public class WeeklyTask : Task
{
    private List<int> ScheduledDays { get; set; }
    
    public WeeklyTask(string taskName,string category,string description,DateTime start,DateTime end) : base(taskName,category,description,start,end)
    {
    }

    public override string ToString()
    {
        return base.ToString()
            + Sb.AppendLine();
    }
}