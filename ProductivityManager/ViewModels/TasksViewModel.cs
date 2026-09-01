using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProductivityManager.Commands;
using ProductivityManager.Models.Task;

namespace ProductivityManager.ViewModels;

public class TasksViewModel : INotifyPropertyChanged
{
    private readonly TaskService _taskService;
    public TaskEditorViewModel Editor { get; }
    public ObservableCollection<TaskModel> Tasks => _taskService.Tasks;
    
    private TaskModel? _selectedTask;

    public TaskModel? SelectedTask
    {
        get => _selectedTask;
        set
        {
            _selectedTask = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddTaskCommand { get; }
    
    public ICommand EditTaskCommand { get; }
    
    public ICommand DeleteTaskCommand { get; }

    public TasksViewModel(TaskService taskService)
    {
        _taskService = taskService;
        Editor = new TaskEditorViewModel(taskService);

        AddTaskCommand = new RelayCommand(_ => OpenCreateTask());
        EditTaskCommand = new RelayCommand(task =>
        {
            if (task is TaskModel taskModel)
            {
                OpenEditTask(taskModel);
            }
        });
        DeleteTaskCommand = new RelayCommand(task =>
        {
            if (task is TaskModel taskModel)
            {
                OnTaskDeleted(taskModel);
            }
        });
    }
    
    private void OpenCreateTask()
    {
        Editor.OpenCreate();
    }

    private void OnTaskCreated(TaskModel task)
    {
        Tasks.Add(task);
    }

    public void OpenEditTask(TaskModel task)
    {
        Editor.OpenEdit(task);
    }

    private void OnTaskEdited(TaskModel oldTask, TaskModel newTask)
    {
        int index = Tasks.IndexOf(oldTask);

        if (index >= 0)
        {
            Tasks[index] = newTask;
        }
    }

    private void OpenDeleteTask()
    {
        Editor.OpenDelete();
    }

    public void OnTaskDeleted(TaskModel task)
    {
        Tasks.Remove(task);
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