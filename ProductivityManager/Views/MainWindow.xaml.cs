using System.Text;
using System.Windows;

namespace ProductivityManager.Views;
 
public partial class MainWindow : Window
{
    HomeView _homeView = new HomeView();
    TasksView _tasksView = new TasksView();
    SettingsView _settingsView = new SettingsView();
    public MainWindow()
    {
        InitializeComponent();
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