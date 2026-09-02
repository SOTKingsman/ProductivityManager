using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace ProductivityManager.Models.Task;

public class TaskModel
{
    protected StringBuilder Sb = new StringBuilder();
    public string TaskName
    {
        get;
        set
        {
            if (value != null || !value.Trim().Equals(""))
            {
                field = value;
            }
        }
    }
    public string Category
    {
        get;
        set
        {
            if (value != null || !value.Trim().Equals(""))
            {
                field = value;
            }
        }
    }
    public string Description
    {
        get;
        set
        {
            if (value != null || !value.Trim().Equals(""))
            {
                field = value;
            }
        }
    }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    
    public HashSet<DateTime> CompletedOccurrences { get; } = new();

    public TaskModel(string taskName,string category,string description, DateTime start, DateTime end)
    {
        TaskName = taskName;
        Category = category;
        Description = description;
        StartDateTime = start;
        EndDateTime = end;
    }
    
    public override string ToString()
    {
        return Sb.Append("Task: ").AppendLine(TaskName)
            .Append("Category: ").AppendLine(Category)
            .Append("Description: ").AppendLine(Description)
            .Append("Timeframe: ").Append(StartDateTime.ToShortDateString()).Append(", ")
            .Append(StartDateTime.ToShortTimeString()).Append(" - ")
            .Append(EndDateTime.ToShortDateString()).Append(", ")
            .Append(EndDateTime.ToShortTimeString())
            .ToString();
    }
}