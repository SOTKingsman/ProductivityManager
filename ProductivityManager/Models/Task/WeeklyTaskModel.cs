namespace ProductivityManager.Models.Task;

public class WeeklyTaskModel : TaskModel
{
    private List<int> ScheduledDays { get; set; }
    
    public WeeklyTaskModel(string taskName,string category,string description,DateTime start,DateTime end) : base(taskName,category,description,start,end)
    {
    }

    public override string ToString()
    {
        return base.ToString()
            + Sb.AppendLine();
    }
}