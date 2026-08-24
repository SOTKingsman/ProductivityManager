using System.Windows;
using System.Windows.Controls;
using ProductivityManager.Models.Task;

namespace ProductivityManager.Views;

public partial class TasksView : UserControl
{
    public List<TaskModel> Tasks = new List<TaskModel>();

    public TasksView()
    {
        InitializeComponent();
    }
    
    private void TaskItem_OnSelected(object sender, RoutedEventArgs e)
    {
        if (TaskList.SelectedItem is TaskModel task) 
        {
            MessageBox.Show(task.TaskName);
        }
    }
    
    private void TaskList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }

    private void BtnAddTask_OnClick(object sender, RoutedEventArgs e)
    {
        CreateTaskOverlay.Visibility = Visibility.Visible;
    }

    private void BtnCancelCreateTask_OnClick(object sender, RoutedEventArgs e)
    {
        CreateTaskOverlay.Visibility = Visibility.Collapsed;
        ClearInputs();
    }

    private void BtnCreateTask_OnClick(object sender, RoutedEventArgs e)
    {
        TimeSpan startTime;
        TimeSpan endTime;

        if (!DateTime.TryParse(StartTimeInput.Text, out DateTime parsedStartTime) ||
            !DateTime.TryParse(EndTimeInput.Text, out DateTime parsedEndTime))
        {
            MessageBox.Show("Please enter valid start and end times.");
            return;
        }

        startTime = parsedStartTime.TimeOfDay;
        endTime = parsedEndTime.TimeOfDay;
        
        DateTime? startDate = StartDateInput.SelectedDate;
        DateTime? endDate = EndDateInput.SelectedDate;
        
        if (!startDate.HasValue || !endDate.HasValue)
        {
            MessageBox.Show("Please select both a start date and an end date.");
            return;
        }
        
        DateTime finalStart = startDate.Value.Date + startTime;
        DateTime finalEnd = endDate.Value.Date + endTime;
        
        if (finalEnd < finalStart)
        {
            MessageBox.Show("The end time cannot be before the start time.");
            return;
        }
        
        TaskModel task = new TaskModel(
            TaskNameInput.Text,
            CategoryInput.Text,
            DescriptionInput.Text,
            finalStart,
            finalEnd
        );

        Tasks.Add(task);
        TaskList.Items.Add(task);
        
        CreateTaskOverlay.Visibility = Visibility.Collapsed;
        ClearInputs();
    }

    private void ClearInputs()
    {
        TaskNameInput.Text = "";
        CategoryInput.Text = "";
        DescriptionInput.Text = "";
        StartDateInput.Text = "";
        EndDateInput.Text = "";
        StartTimeInput.Text = "12:00 PM";
        EndTimeInput.Text = "1:00 PM";
    }

    
}