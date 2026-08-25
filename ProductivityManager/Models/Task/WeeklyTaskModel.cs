namespace ProductivityManager.Models.Task;

public class WeeklyTaskModel : TaskModel
{
    public List<DayOfWeek> ScheduledDays { get; set; }
    public DateTime RepeatUntil { get; set; }
    
    public WeeklyTaskModel(string taskName, string category, string description, DateTime start, DateTime end,
        List<DayOfWeek> scheduledDays, DateTime repeatUntil) : base(taskName, category, description, start, end)
    {
        ScheduledDays = scheduledDays;
        RepeatUntil = repeatUntil;
    }

    public override string ToString()
    {
        return base.ToString()
            + Sb.AppendLine();
    }
}