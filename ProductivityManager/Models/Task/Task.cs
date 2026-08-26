using System.DirectoryServices.ActiveDirectory;
using System.Text;
using ProductivityManager.Models;
using ProductivityManager.Views.Models.TaskDatabase;

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
    private string Category
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
    
    public Database.TaskData GetTaskData()
    {
        return new Database.TaskData
        {
            TaskName = TaskName,
            Category = Category,
            Description = Description,
            StartDateTime = StartDateTime,
            EndDateTime = EndDateTime
        };
    }
    
    
}