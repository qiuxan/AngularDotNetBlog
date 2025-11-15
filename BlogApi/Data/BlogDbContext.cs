namespace BlogApi.Data;

using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

public class BlogDbContext: DbContext
{
    public BlogDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Post> Posts => Set<Post>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Slug unique
        modelBuilder.Entity<Post>()
            .HasIndex(p => p.Slug)
            .IsUnique();
    }


}