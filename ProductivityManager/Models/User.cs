namespace ProductivityManager.Views.Models;

public class User
{
    private string Name { get; set; }
    private string Password { get; set; }
    
    public User(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }
}