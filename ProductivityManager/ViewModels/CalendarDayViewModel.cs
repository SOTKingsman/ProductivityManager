using System.Collections.ObjectModel;
using ProductivityManager.Models.Task;

namespace ProductivityManager.ViewModels;

public class CalendarDayViewModel
{
    public DateTime Date { get; }

    public string DayName => Date.ToString("dddd");

    public string DateText => Date.ToString("MMM d");

    public string DayNumber => Date.Day.ToString();

    public bool IsInCurrentMonth { get; }

    public ObservableCollection<TaskOccurrence> Occurrences { get; } = new();

    public CalendarDayViewModel(
        DateTime date,
        bool isInCurrentMonth = true)
    {
        Date = date;
        IsInCurrentMonth = isInCurrentMonth;
    }
}