namespace BlogApi.Data;

using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

public class BlogDbContext: DbContext
{
    public BlogDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Slug unique
        modelBuilder.Entity<Post>()
            .HasIndex(p => p.Slug)
            .IsUnique();
        
    //  Admin123!  SHA256
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserName = "admin",
                PasswordHash = "3eb3fe66b31e3b4d10fa70b5cad49c7112294af6ae4e476a1c405155d45aa121"
            }
        );
    }


}