using System.Text;
using System.Windows;
using ProductivityManager.Models.Task;
using ProductivityManager.Services;

namespace ProductivityManager.Views;
 
public partial class MainWindow : Window
{
    private readonly TaskService _taskService = new();

    private readonly HomeView _homeView;
    private readonly TasksView _tasksView;
    private readonly SettingsView _settingsView;
    public MainWindow()
    {
        InitializeComponent();

        _homeView = new HomeView(_taskService);
        _tasksView = new TasksView(_taskService);
        _settingsView = new SettingsView();

        MainContent.Content = _homeView;
    }

    private void SideBar_OnHomeRequested(object? sender, EventArgs e)
    {
        MainContent.Content = _homeView;
    }

    private void SideBar_OnTasksRequested(object? sender, EventArgs e)
    {
        MainContent.Content = _tasksView;
    }

    private void SideBar_OnSettingsRequested(object? sender, EventArgs e)
    {
        MainContent.Content = _settingsView;
    }
}