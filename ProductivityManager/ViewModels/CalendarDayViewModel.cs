using System.Collections.ObjectModel;
using ProductivityManager.Models.Task;

namespace ProductivityManager.ViewModels;

public class CalendarDayViewModel
{
    public DateTime Date { get; }

    public string DayName => Date.ToString("dddd");

    public string DateText => Date.ToString("MMM d");

    public ObservableCollection<TaskOccurrence> Occurrences { get; } = new();

    public CalendarDayViewModel(DateTime date)
    {
        Date = date;
    }
}