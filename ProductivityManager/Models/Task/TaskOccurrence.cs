namespace ProductivityManager.Models.Task;

public class TaskOccurrence
{
    public TaskModel Task { get; }

    public DateTime StartDateTime { get; }

    public DateTime EndDateTime { get; }

    public TaskOccurrence(TaskModel task, DateTime startDateTime, DateTime endDateTime)
    {
        Task = task;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }
}