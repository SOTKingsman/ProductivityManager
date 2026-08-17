namespace ProductivityManager.Models.DAO;

using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
public class UserDao
{
    private readonly string _connectionString = @"Data Source=identifier.sqlite";

    public async Task DeleteUser(int id)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        string sql = "DELETE FROM Users WHERE user_id = @id";
        await using var command = new SqliteCommand(sql, connection);
        
        command.Parameters.AddWithValue("@user_id", id);
        
        await command.ExecuteNonQueryAsync();
    }
}