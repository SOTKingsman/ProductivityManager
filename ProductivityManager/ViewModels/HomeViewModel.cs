using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProductivityManager.Commands;
using ProductivityManager.Models;
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
    public ICommand ShowWeeklyCommand { get; }
    public ICommand ShowMonthlyCommand { get; }

    public ObservableCollection<CalendarDayViewModel> Days { get; } = new();
    
    private CalendarViewType _selectedCalendarView = CalendarViewType.Weekly;

    public CalendarViewType SelectedCalendarView
    {
        get => _selectedCalendarView;
        set
        {
            _selectedCalendarView = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(IsWeeklyView));
            OnPropertyChanged(nameof(IsMonthlyView));

            LoadCalendar();
        }
    }

    public bool IsWeeklyView => SelectedCalendarView == CalendarViewType.Weekly;

    public bool IsMonthlyView => SelectedCalendarView == CalendarViewType.Monthly;

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
        
        ShowWeeklyCommand = new RelayCommand(_ =>
        {
            SelectedCalendarView = CalendarViewType.Weekly;
        });

        ShowMonthlyCommand = new RelayCommand(_ =>
        {
            SelectedCalendarView = CalendarViewType.Monthly;
        });

        _taskService.Tasks.CollectionChanged += TasksChanged;

        LoadWeek(DateTime.Now);
    }

    private void TasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        LoadCalendar();
    }
    
    private void LoadCalendar()
    {
        if (IsWeeklyView)
        {
            LoadWeek(DateTime.Now);
        }
        else
        {
            LoadMonth(DateTime.Now);
        }
    }

    private void LoadWeek(DateTime date)
    {
        Days.Clear();
        
        int daysSinceSunday = (int)date.DayOfWeek;

        WeekStart = date.Date.AddDays(-daysSinceSunday);

        DateTime weekEnd =
            WeekStart.AddDays(7).AddTicks(-1);
        
        for (int i = 0; i < 7; i++)
        {
            Days.Add(
                new CalendarDayViewModel(
                    WeekStart.AddDays(i)
                )
            );
        }
        
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
        
        SortOccurrences();
    }
    
    private void LoadMonth(DateTime date)
    {
        Days.Clear();

        DateTime firstOfMonth =
            new DateTime(date.Year, date.Month, 1);

        int daysBeforeMonth =
            (int)firstOfMonth.DayOfWeek;

        DateTime calendarStart =
            firstOfMonth.AddDays(-daysBeforeMonth);

        // Always generate six weeks.
        DateTime calendarEnd =
            calendarStart.AddDays(42).AddTicks(-1);

        for (int i = 0; i < 42; i++)
        {
            DateTime dayDate =
                calendarStart.AddDays(i);

            bool isInCurrentMonth =
                dayDate.Month == date.Month &&
                dayDate.Year == date.Year;

            Days.Add(
                new CalendarDayViewModel(
                    dayDate,
                    isInCurrentMonth
                )
            );
        }

        foreach (TaskModel task in _taskService.Tasks)
        {
            List<TaskOccurrence> occurrences =
                _scheduleService.GetOccurrences(
                    task,
                    calendarStart,
                    calendarEnd
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

        SortOccurrences();
    }
    
    private void SortOccurrences()
    {
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