namespace BlogApi.Models;

public class Post
{
    public int Id { get;set; }
    
    public string Slug { get;set; } = default;
    public string Title { get;set; } = default;
    public string Summary { get; set; } = default;
    public string Content { get;set; } = default;
    
    public DateTime CreatedAt { get;set; } = default;
    public DateTime? UpdatedAt { get;set; } = default;
    
}