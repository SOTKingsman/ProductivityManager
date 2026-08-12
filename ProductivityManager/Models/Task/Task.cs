using System.DirectoryServices.ActiveDirectory;

namespace ProductivityManager.Views.Models;

public class Task
{
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

    public Task(String taskName,String description,int startHour,int startMinute,int endHour,int endMinute)
    {
        TaskName = taskName;
        Description = description;
        StartTime = SelectTime(startHour, startMinute);
        EndTime = SelectTime(endHour, endMinute);
    }

    public TimeOnly SelectTime(int hour,int minute)
    {
        TimeOnly time = new TimeOnly(hour, minute);
        return  time;
    }
}