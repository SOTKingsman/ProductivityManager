using ProductivityManager.Views.Models;

namespace ProductivityManager.Models.DAO;

using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
public class UserDao
{
    private readonly string _connectionString = @"Data Source=identifier.sqlite";

    public async Task CreateUser(string name, string password)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        string sql = "INSERT INTO users (name, password) VALUES (@name, @password)";
        await using var command = new SqliteCommand(sql, connection);
        
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@password", password);
        
        await command.ExecuteNonQueryAsync();
    }

    public User GetUserById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM users WHERE user_id = @id";
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new User(reader.GetString(1), reader.GetString(2));
        }

        return null;
    }

    public List<User> GetAllUsers()
    {
        var users = new List<User>();
        
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM users";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var user = new User(reader.GetString(1), reader.GetString(2));
            users.Add(user);
        }
        return users;
    }

    public async Task UpdateUser(int id, string name, string password)
    {
        await using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        
        string sql = "UPDATE users SET name=@name, password=@password WHERE user_id = @id";
        await using var command = new SqliteCommand(sql, connection);
        
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@password", password);
        command.Parameters.AddWithValue("@id", id);
        
        await command.ExecuteNonQueryAsync();
    }
    
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