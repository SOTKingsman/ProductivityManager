using System.Windows;
using System.Windows.Controls;
using ProductivityManager.Models.Task;
using ProductivityManager.ViewModels;

namespace ProductivityManager.Views;

public partial class TasksView : UserControl
{
    private readonly TasksViewModel _viewModel = new();

    public TasksView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }
}