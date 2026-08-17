using ProductivityManager.Models.Task;

namespace ProductivityManager.Models.DAO;

using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

public class TaskDao
{
    private readonly string _connectionString = "Data Source=identifier.sqlite";

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