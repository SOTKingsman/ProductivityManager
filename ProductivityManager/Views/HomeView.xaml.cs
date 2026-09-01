using System.Windows.Controls;
using ProductivityManager.Models.Task;
using ProductivityManager.ViewModels;

namespace ProductivityManager.Views;

public partial class HomeView : UserControl
{
    public HomeView(TaskService taskService)
    {
        InitializeComponent();

        DataContext = new HomeViewModel(taskService);
    }
}