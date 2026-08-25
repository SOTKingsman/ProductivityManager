using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ProductivityManager.Commands;
using ProductivityManager.Models.Task;

namespace ProductivityManager.ViewModels;

public class TasksViewModel : INotifyPropertyChanged
{
    public TaskEditorViewModel Editor { get; } = new();
    public ObservableCollection<TaskModel> Tasks { get; } = new();
    
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

    public TasksViewModel()
    {
        AddTaskCommand = new RelayCommand(_ => OpenCreateTask());
        EditTaskCommand = new RelayCommand(task =>
        {
            if (task is TaskModel taskModel)
            {
                OpenEditTask(taskModel);
            }
        });

        Editor.TaskCreated += OnTaskCreated;
        Editor.TaskEdited += OnTaskEdited;
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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName)
        );
    }
}