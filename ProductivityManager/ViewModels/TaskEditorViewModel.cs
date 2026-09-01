using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProductivityManager.Commands;
using ProductivityManager.Models.Task;
using ProductivityManager.Services;

namespace ProductivityManager.ViewModels;

public class TaskEditorViewModel : INotifyPropertyChanged
{
    private TaskModel? _editingTask;
    
    private readonly TaskService _taskService;
    public string SubmitButtonText => _editingTask == null ? "Create" : "Confirm";
    public string EditorTitle => _editingTask == null ? "Create Task" : "Edit Task";

    public ICommand CreateTaskCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand DeleteTaskCommand { get; }

    #region Task Properties
    private string _taskName = "";
    public string TaskName
    {
        get => _taskName;
        set
        {
            _taskName = value;
            OnPropertyChanged();
        }
    }

    private string _category = "";
    public string Category
    {
        get => _category;
        set
        {
            _category = value;
            OnPropertyChanged();
        }
    }

    private string _description = "";
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _startDate;
    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _endDate;
    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged();
        }
    }

    private string _startTime = "12:00 PM";
    public string StartTime
    {
        get => _startTime;
        set
        {
            _startTime = value;
            OnPropertyChanged();
        }
    }

    private string _endTime = "1:00 PM";
    public string EndTime
    {
        get => _endTime;
        set
        {
            _endTime = value;
            OnPropertyChanged();
        }
    }
    
    private bool _sunday;
    public bool Sunday
    {
        get => _sunday;
        set
        {
            _sunday = value;
            OnPropertyChanged();
        }
    }

    private bool _monday;
    public bool Monday
    {
        get => _monday;
        set
        {
            _monday = value;
            OnPropertyChanged();
        }
    }

    private bool _tuesday;
    public bool Tuesday
    {
        get => _tuesday;
        set
        {
            _tuesday = value;
            OnPropertyChanged();
        }
    }

    private bool _wednesday;
    public bool Wednesday
    {
        get => _wednesday;
        set
        {
            _wednesday = value;
            OnPropertyChanged();
        }
    }

    private bool _thursday;
    public bool Thursday
    {
        get => _thursday;
        set
        {
            _thursday = value;
            OnPropertyChanged();
        }
    }

    private bool _friday;
    public bool Friday
    {
        get => _friday;
        set
        {
            _friday = value;
            OnPropertyChanged();
        }
    }

    private bool _saturday;
    public bool Saturday
    {
        get => _saturday;
        set
        {
            _saturday = value;
            OnPropertyChanged();
        }
    }
    
    private int _dayOfMonth = 1;

    public int DayOfMonth
    {
        get => _dayOfMonth;
        set
        {
            _dayOfMonth = value;
            OnPropertyChanged();
        }
    }
    
    private bool _noEndDate;

    public bool NoEndDate
    {
        get => _noEndDate;
        set
        {
            _noEndDate = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(HasEndDate));
        }
    }

    public bool HasEndDate => !NoEndDate;
    
    private TaskType _selectedTaskType = TaskType.Daily;

    public TaskType SelectedTaskType
    {
        get => _selectedTaskType;
        set
        {
            _selectedTaskType = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(IsDaily));
            OnPropertyChanged(nameof(IsWeekly));
            OnPropertyChanged(nameof(IsMonthly));
        }
    }

    public Array TaskTypes => Enum.GetValues(typeof(TaskType));

    public bool IsDaily => SelectedTaskType == TaskType.Daily;
    public bool IsWeekly => SelectedTaskType == TaskType.Weekly;
    public bool IsMonthly => SelectedTaskType == TaskType.Monthly;
    #endregion

    private bool _isOpen = false;
    public bool IsOpen
    {
        get => _isOpen;
        set
        {
            _isOpen = value;
            OnPropertyChanged();
        }
    }
    
    private bool _isEditing = false;

    public bool IsEditing
    {
        get => _isEditing;
        set
        {
            _isEditing = value;
            OnPropertyChanged();
        }
    }

    private string _errorMessage = "";
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public TaskEditorViewModel(TaskService taskService)
    {
        _taskService = taskService;

        CreateTaskCommand = new RelayCommand(_ => CreateTask());
        CancelCommand = new RelayCommand(_ => Cancel());
        DeleteTaskCommand = new RelayCommand(_ => DeleteTask());

        ClearInputs();
    }

    public void OpenCreate()
    {
        IsEditing = false;
        _editingTask = null;
        OnPropertyChanged(nameof(EditorTitle));
        OnPropertyChanged(nameof(SubmitButtonText));

        ClearInputs();
        IsOpen = true;
    }
    
    private List<DayOfWeek> GetSelectedDays()
    {
        List<DayOfWeek> days = new();

        if (Sunday)
            days.Add(DayOfWeek.Sunday);

        if (Monday)
            days.Add(DayOfWeek.Monday);

        if (Tuesday)
            days.Add(DayOfWeek.Tuesday);

        if (Wednesday)
            days.Add(DayOfWeek.Wednesday);

        if (Thursday)
            days.Add(DayOfWeek.Thursday);

        if (Friday)
            days.Add(DayOfWeek.Friday);

        if (Saturday)
            days.Add(DayOfWeek.Saturday);

        return days;
    }
    
    private void CreateTask()
    {
        ErrorMessage = "";
    
        if (!DateTime.TryParse(StartTime, out DateTime parsedStartTime) ||
            !DateTime.TryParse(EndTime, out DateTime parsedEndTime))
        {
            ErrorMessage = "Please enter valid start and end times.";
            return;
        }
    
        if (!StartDate.HasValue)
        {
            ErrorMessage = "Please select a start date.";
            return;
        }
    
        // Daily tasks must always have an end date.
        if (IsDaily && !EndDate.HasValue)
        {
            ErrorMessage = "Please select an end date.";
            return;
        }
    
        // Recurring tasks only need an end date when
        // No End Date is NOT checked.
        if ((IsWeekly || IsMonthly) &&
            !NoEndDate &&
            !EndDate.HasValue)
        {
            ErrorMessage = "Please select an end date.";
            return;
        }
    
        TimeSpan startTime = parsedStartTime.TimeOfDay;
        TimeSpan endTime = parsedEndTime.TimeOfDay;
    
        if (endTime < startTime)
        {
            ErrorMessage = "The end time cannot be before the start time.";
            return;
        }
    
        DateTime finalStart =
            StartDate.Value.Date + startTime;
    
        TaskModel task;
    
        switch (SelectedTaskType)
        {
            case TaskType.Weekly:
            {
                List<DayOfWeek> selectedDays =
                    GetSelectedDays();
    
                if (selectedDays.Count == 0)
                {
                    ErrorMessage =
                        "Please select at least one day of the week.";
                    return;
                }
    
                DateTime? repeatUntil =
                    NoEndDate
                        ? null
                        : EndDate!.Value.Date;
    
                if (repeatUntil.HasValue &&
                    repeatUntil.Value < StartDate.Value.Date)
                {
                    ErrorMessage =
                        "The end date cannot be before the start date.";
                    return;
                }
    
                // For recurring tasks the date of EndDateTime
                // isn't the schedule end. RepeatUntil handles that.
                // EndDateTime just stores the occurrence's ending time.
                DateTime finalEnd =
                    StartDate.Value.Date + endTime;
    
                task = new WeeklyTaskModel(
                    TaskName,
                    Category,
                    Description,
                    finalStart,
                    finalEnd,
                    selectedDays,
                    repeatUntil
                );
    
                break;
            }
    
            case TaskType.Monthly:
            {
                if (DayOfMonth < 1 || DayOfMonth > 31)
                {
                    ErrorMessage =
                        "Day of month must be between 1 and 31.";
                    return;
                }
    
                DateTime? repeatUntil =
                    NoEndDate
                        ? null
                        : EndDate!.Value.Date;
    
                if (repeatUntil.HasValue &&
                    repeatUntil.Value < StartDate.Value.Date)
                {
                    ErrorMessage =
                        "The end date cannot be before the start date.";
                    return;
                }
    
                DateTime finalEnd =
                    StartDate.Value.Date + endTime;
    
                task = new MonthlyTaskModel(
                    TaskName,
                    Category,
                    Description,
                    finalStart,
                    finalEnd,
                    DayOfMonth,
                    repeatUntil
                );
    
                break;
            }
    
            default:
            {
                DateTime finalEnd =
                    EndDate!.Value.Date + endTime;
    
                if (finalEnd < finalStart)
                {
                    ErrorMessage =
                        "The end date cannot be before the start date.";
                    return;
                }
    
                task = new TaskModel(
                    TaskName,
                    Category,
                    Description,
                    finalStart,
                    finalEnd
                );
    
                break;
            }
        }
    
        if (_editingTask == null)
        {
            _taskService.AddTask(task);
        }
        else
        {
            _taskService.UpdateTask(_editingTask, task);
        }
    
        IsEditing = false;
        _editingTask = null;
        IsOpen = false;
    
        ClearInputs();
    }

    private void Cancel()
    {
        IsEditing = false;
        _editingTask = null;
        IsOpen = false;
        ClearInputs();
    }

    private void ClearInputs()
    {
        TaskName = "";
        Category = "";
        Description = "";

        SelectedTaskType = TaskType.Daily;

        StartDate = DateTime.Now;
        EndDate = DateTime.Now.AddDays(1);

        StartTime = "12:00 PM";
        EndTime = "1:00 PM";

        Sunday = false;
        Monday = false;
        Tuesday = false;
        Wednesday = false;
        Thursday = false;
        Friday = false;
        Saturday = false;

        DayOfMonth = 1;

        NoEndDate = false;

        ErrorMessage = "";
    }
    
    public void OpenEdit(TaskModel task)
    {
        IsEditing = true;
        _editingTask = task;

        OnPropertyChanged(nameof(EditorTitle));
        OnPropertyChanged(nameof(SubmitButtonText));

        TaskName = task.TaskName;
        Category = task.Category;
        Description = task.Description;

        StartDate = task.StartDateTime.Date;
        StartTime = task.StartDateTime.ToString("h:mm tt");
        EndTime = task.EndDateTime.ToString("h:mm tt");
        
        Sunday = false;
        Monday = false;
        Tuesday = false;
        Wednesday = false;
        Thursday = false;
        Friday = false;
        Saturday = false;

        NoEndDate = false;
        DayOfMonth = 1;

        if (task is WeeklyTaskModel weeklyTask)
        {
            SelectedTaskType = TaskType.Weekly;

            Sunday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Sunday);

            Monday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Monday);

            Tuesday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Tuesday);

            Wednesday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Wednesday);

            Thursday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Thursday);

            Friday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Friday);

            Saturday =
                weeklyTask.ScheduledDays.Contains(DayOfWeek.Saturday);

            NoEndDate = !weeklyTask.RepeatUntil.HasValue;

            EndDate = weeklyTask.RepeatUntil;
        }
        else if (task is MonthlyTaskModel monthlyTask)
        {
            SelectedTaskType = TaskType.Monthly;

            DayOfMonth = monthlyTask.DayOfMonth;

            NoEndDate = !monthlyTask.RepeatUntil.HasValue;

            EndDate = monthlyTask.RepeatUntil;
        }
        else
        {
            SelectedTaskType = TaskType.Daily;

            EndDate = task.EndDateTime.Date;
        }

        IsOpen = true;
    }

    public void OpenDelete()
    {
       //If a pop-up for confirmation is wanted
    }

    private void DeleteTask()
    {
        if (_editingTask != null)
        {
            _taskService.DeleteTask(_editingTask);
        }

        Cancel();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName)
        );
    }
}