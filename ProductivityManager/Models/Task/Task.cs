using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace ProductivityManager.Models.Task;

public class Task
{
    private StringBuilder _sb = new StringBuilder();
    private String TaskName
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
    private DateTime StartDateTime
    {
        get;
        set
        {
            if (value != null)
            {
                field = value;
            }
        }
    }
    private DateTime EndDateTime
    {
        get;
        set
        {
            if (value != null)
            {
                field = value;
            }
        }
    }

    public Task(String taskName, String category, String description,DateTime start,DateTime end)
    {
        TaskName = taskName;
        Category = category;
        Description = description;
        StartDateTime = start;
        EndDateTime = end;
    }
    
    public override string ToString()
    {
        return _sb.Append("Task: ").Append(TaskName)
            .AppendLine("Category: ").Append(Category)
            .AppendLine("Description: ").Append(Description)
            .AppendLine("Timeframe: ").Append(StartDateTime).Append(" - ").Append(EndDateTime)
            .ToString();
    }
}