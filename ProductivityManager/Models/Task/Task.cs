using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace ProductivityManager.Models.Task;

public abstract class Task
{
    protected StringBuilder Sb = new StringBuilder();
    private string TaskName
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
    private String Category
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
    private String Description
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
    private string Description
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
    private DateTime StartDateTime { get; set; }
    private DateTime EndDateTime { get; set; }

    public Task(string taskName,string category,string description,DateTime start,DateTime end)
    {
        TaskName = taskName;
        Category = category;
        Description = description;
        StartDateTime = start;
        EndDateTime = end;
    }
    
    public override string ToString()
    {
        return Sb.Append("Task: ").Append(TaskName)
            .AppendLine("Category: ").Append(Category)
            .AppendLine("Description: ").Append(Description)
            .AppendLine("Timeframe: ").Append(StartDateTime).Append(" - ").Append(EndDateTime)
            .ToString();
    }
}