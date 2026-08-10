using System.Windows;
using System.Windows.Controls;

namespace ProductivityManager.Views.UserControls;

public partial class SideBar : UserControl
{
    public event EventHandler? HomeRequested;
    public event EventHandler? TasksRequested;
    public event EventHandler? SettingsRequested;
    
    public SideBar()
    {
        InitializeComponent();
    }

    private void BtnHome_OnClick(object sender, RoutedEventArgs e)
    {
        HomeRequested?.Invoke(this, EventArgs.Empty);
    }

    private void BtnTasks_OnClick(object sender, RoutedEventArgs e)
    {
        TasksRequested?.Invoke(this, EventArgs.Empty);
    }

    private void BtnSettings_OnClick(object sender, RoutedEventArgs e)
    {
        SettingsRequested?.Invoke(this, EventArgs.Empty);
    }
}