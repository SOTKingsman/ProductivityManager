using ProductivityManager.Models.Task;

namespace ProductivityManager.Models.DAO;

using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

public class TaskDao
{
    private readonly string _connectionString = "Data Source=identifier.sqlite";

    public async Task CreateTask(string name, string category, string description, DateTime startDate, DateTime endDate,
        int userId, string status)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        string sql = "INSERT INTO tasks (name, category, description, startdate, enddate, user_id, status) VALUES (@name, @category, @description, @startdate, @enddate, @user_id, @status)";
        await using var command = new SqliteCommand(sql, connection);
        
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@startdate", startDate);
        command.Parameters.AddWithValue("@enddate", endDate);
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@status", status);
        
        await command.ExecuteNonQueryAsync();
    }

    public TaskModel GetTaskById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        string sql = "SELECT * FROM tasks WHERE task_id = $id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read() &&  reader["status"].ToString().Equals("weekly"))
        {
            return new WeeklyTaskModel(reader.GetString(0),  reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), new List<DayOfWeek>(), reader.GetDateTime(4));
        } else if (reader.Read() && reader["status"].ToString().Equals("monthly"))
        {
            return new MonthlyTaskModel(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetDateTime(3).Day, reader.GetDateTime(4));
        }
        return null;
    }

    public List<MonthlyTaskModel> GetAllMonthlyTasks()
    {
        var monthlyTasks = new List<MonthlyTaskModel>();
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        string sql = "SELECT * FROM tasks WHERE status = 'monthly'";
        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var task = new MonthlyTaskModel(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), reader.GetDateTime(3).Day, reader.GetDateTime(4));
            monthlyTasks.Add(task);
        }
        return monthlyTasks;
    }

    public List<WeeklyTaskModel> GetAllWeeklyTasks()
    {
        var weeklyTasks = new List<WeeklyTaskModel>();
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        string sql = "SELECT * FROM tasks WHERE status = 'weekly'";
        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var task = new WeeklyTaskModel(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetDateTime(4), new List<DayOfWeek>(),  reader.GetDateTime(4));
            weeklyTasks.Add(task);
        }
        return weeklyTasks;
    }

    public async Task UpdateTask(int id, string name, string category, string description, DateTime startDate,
        DateTime endDate, int userId, string status)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        string sql = "UPDATE tasks SET name=@name, category=@category, description=@description, startdate=@startdate, enddate=@enddate, user_id=@user_id, status=@status WHERE task_id = @id";
        await using var command = new SqliteCommand(sql, connection);
        
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@startdate", startDate);
        command.Parameters.AddWithValue("@enddate", endDate);
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@status", status);
        command.Parameters.AddWithValue("@id", id);
        
        await command.ExecuteNonQueryAsync();
    }
    
    public async Task DeleteTaskByStatus(int id, string status)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        string sql = "DELETE FROM Tasks WHERE task_id = $id AND status = $status";
        await using var command = new SqliteCommand(sql, connection);
        
        command.Parameters.AddWithValue("@task_id", id);
        command.Parameters.AddWithValue("@status", status);
        
        await command.ExecuteNonQueryAsync();
    }
}