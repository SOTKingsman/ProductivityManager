using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProductivityManager.Commands;
using ProductivityManager.Models.Task;
using ProductivityManager.Services;

namespace ProductivityManager.ViewModels;

public class HomeViewModel : INotifyPropertyChanged
{
    private readonly TaskService _taskService;
    private readonly TaskScheduleService _scheduleService;
    
    public TaskEditorViewModel Editor { get; }
    public ICommand AddTaskCommand { get; }
    public ICommand OpenTaskCommand { get; }

    public ObservableCollection<CalendarDayViewModel> Days { get; } = new();

    private DateTime _weekStart;

    public DateTime WeekStart
    {
        get => _weekStart;
        private set
        {
            _weekStart = value;
            OnPropertyChanged();
        }
    }

    public string CurrentDateText => DateTime.Now.ToString("dddd, MMMM d, yyyy");

    public HomeViewModel(TaskService taskService)
    {
        _taskService = taskService;
        _scheduleService = new TaskScheduleService();

        Editor = new TaskEditorViewModel(taskService);

        AddTaskCommand = new RelayCommand(_ =>
        {
            Editor.OpenCreate();
        });

        OpenTaskCommand = new RelayCommand(task =>
        {
            if (task is TaskModel taskModel)
            {
                Editor.OpenEdit(taskModel);
            }
        });

        _taskService.Tasks.CollectionChanged += TasksChanged;

        LoadWeek(DateTime.Now);
    }

    private void TasksChanged(
        object? sender,
        NotifyCollectionChangedEventArgs e)
    {
        LoadWeek(WeekStart);
    }

    private void LoadWeek(DateTime date)
    {
        Days.Clear();

        // Sunday = 0, Monday = 1, etc.
        int daysSinceSunday = (int)date.DayOfWeek;

        WeekStart = date.Date.AddDays(-daysSinceSunday);

        DateTime weekEnd =
            WeekStart.AddDays(7).AddTicks(-1);

        // Create the 7 calendar days.
        for (int i = 0; i < 7; i++)
        {
            Days.Add(
                new CalendarDayViewModel(
                    WeekStart.AddDays(i)
                )
            );
        }

        // Generate occurrences for every stored task.
        foreach (TaskModel task in _taskService.Tasks)
        {
            List<TaskOccurrence> occurrences =
                _scheduleService.GetOccurrences(
                    task,
                    WeekStart,
                    weekEnd
                );

            foreach (TaskOccurrence occurrence in occurrences)
            {
                CalendarDayViewModel? day =
                    Days.FirstOrDefault(day =>
                        day.Date.Date ==
                        occurrence.StartDateTime.Date
                    );

                if (day != null)
                {
                    day.Occurrences.Add(occurrence);
                }
            }
        }

        // Optional but useful:
        // put each day's tasks in chronological order.
        foreach (CalendarDayViewModel day in Days)
        {
            List<TaskOccurrence> sorted =
                day.Occurrences
                    .OrderBy(o => o.StartDateTime)
                    .ToList();

            day.Occurrences.Clear();

            foreach (TaskOccurrence occurrence in sorted)
            {
                day.Occurrences.Add(occurrence);
            }
        }
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