using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductivityManager.Models.Task;

public class TaskOccurrence : INotifyPropertyChanged
{
    public TaskModel Task { get; }

    public DateTime StartDateTime { get; }

    public DateTime EndDateTime { get; }

    public bool IsCompleted
    {
        get => Task.CompletedOccurrences.Contains(StartDateTime);

        set
        {
            if (value)
            {
                Task.CompletedOccurrences.Add(StartDateTime);
            }
            else
            {
                Task.CompletedOccurrences.Remove(StartDateTime);
            }

            OnPropertyChanged();
        }
    }

    public TaskOccurrence(
        TaskModel task,
        DateTime startDateTime,
        DateTime endDateTime)
    {
        Task = task;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName)
        );
    }
}