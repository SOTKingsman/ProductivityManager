using System.Windows.Controls;
using ProductivityManager.Models.Task;
using ProductivityManager.Services;
using ProductivityManager.ViewModels;

namespace ProductivityManager.Views;

public partial class TasksView : UserControl
{
    public TasksView(TaskService taskService)
    {
        InitializeComponent();

        DataContext = new TasksViewModel(taskService);
    }
}