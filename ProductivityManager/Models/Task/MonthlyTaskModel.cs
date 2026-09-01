namespace ProductivityManager.Models.Task;

public class MonthlyTaskModel : TaskModel
{
    public int DayOfMonth { get; set; }

    public DateTime? RepeatUntil { get; set; }

    public MonthlyTaskModel(string taskName, string category, string description, DateTime start, DateTime end, 
        int dayOfMonth, DateTime? repeatUntil) : base(taskName, category, description, start, end)
    {
        DayOfMonth = dayOfMonth;
        RepeatUntil = repeatUntil;
    }
}