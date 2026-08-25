using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProductivityManager.Commands;
using ProductivityManager.Models.Task;

namespace ProductivityManager.ViewModels;

public class TaskEditorViewModel : INotifyPropertyChanged
{
    public event Action<TaskModel>? TaskCreated;
    public event Action<TaskModel, TaskModel>? TaskEdited;
    public event Action<TaskModel>? TaskDeleted;
    private TaskModel? _editingTask;
    
    private readonly TaskService _taskService;
    public string SubmitButtonText => _editingTask == null ? "Create" : "Confirm";
    public string EditorTitle => _editingTask == null ? "Create Task" : "Edit Task";

    public void DeleteCurrentTask(TaskModel selectedTask)
    {
        _taskService.DeleteTask(selectedTask);
    }

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

    private void Cancel()
    {
        IsEditing = false;
        _editingTask = null;
        IsOpen = false;
        ClearInputs();
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

        if (!StartDate.HasValue || !EndDate.HasValue)
        {
            ErrorMessage = "Please select both a start date and an end date.";
            return;
        }

        TimeSpan startTime = parsedStartTime.TimeOfDay;
        TimeSpan endTime = parsedEndTime.TimeOfDay;

        DateTime finalStart = StartDate.Value.Date + startTime;
        DateTime finalEnd = EndDate.Value.Date + endTime;

        if (finalEnd < finalStart)
        {
            ErrorMessage = "The end time cannot be before the start time.";
            return;
        }

        TaskModel task = new TaskModel(
            TaskName,
            Category,
            Description,
            finalStart,
            finalEnd
        );

        if (_editingTask == null)
        {
            TaskCreated?.Invoke(task);
        }
        else
        {
            TaskEdited?.Invoke(_editingTask, task);
        }

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
        StartDate = DateTime.Now;
        EndDate = DateTime.Now.AddDays(1);
        StartTime = "12:00 PM";
        EndTime = "1:00 PM";
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

        EndDate = task.EndDateTime.Date;
        EndTime = task.EndDateTime.ToString("h:mm tt");

        IsOpen = true;
    }

    public void OpenDelete()
    {
        
    }

    private void DeleteTask()
    {
        IsEditing = false;
        
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