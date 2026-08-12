using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace ProductivityManager.Views.Models;

public class Task
{
    private StringBuilder sb;
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
    private DateOnly Date
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
    private TimeOnly StartTime
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
    private TimeOnly EndTime
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

    public Task(String taskName,String description,int year,int month,int day,int startHour,int startMinute,int endHour,int endMinute)
    {
        TaskName = taskName;
        Description = description;
        Date = SelectDate(year, month, day);
        StartTime = SelectTime(startHour, startMinute);
        EndTime = SelectTime(endHour, endMinute);
    }

    public DateOnly SelectDate(int year, int month, int day)
    {
        DateOnly date = new DateOnly(year, month, day);
        return date;
    }
    
    public TimeOnly SelectTime(int hour,int minute)
    {
        TimeOnly time = new TimeOnly(hour, minute);
        return  time;
    }

    public override string ToString()
    {
        return sb.Append("Task: ").Append(TaskName)
            .AppendLine("Description: ").Append(Description)
            .AppendLine("Date: ").Append(Date)
            .AppendLine("Start Time: ").Append(StartTime)
            .AppendLine("End Time: ").Append(EndTime)
            .ToString();
    }
}