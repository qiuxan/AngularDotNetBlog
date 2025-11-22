namespace BlogApi.Models;

public class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = default!;
    
    public string PasswordHash { get; set; } = default!;
}