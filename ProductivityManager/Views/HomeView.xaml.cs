using System.Windows.Controls;

namespace ProductivityManager.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        
        CurrentDateText.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
    }
}