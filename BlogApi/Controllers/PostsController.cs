using BlogApi.Data;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController: Controller
{
    private readonly BlogDbContext _db;
    public PostsController(BlogDbContext db)
    {
        _db = db;
    }
    
    // GET: api/posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {
        var posts = await _db.Posts
            .Where(p => p.Published)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return Ok(posts);
    }

    // GET: api/posts/hello-world
    [HttpGet("{slug}")]
    public async Task<ActionResult<Post>> GetBySlug(string slug)
    {
        var post = await _db.Posts
            .FirstOrDefaultAsync(p => p.Slug == slug && p.Published);

        if (post == null) return NotFound();
        return Ok(post);
    }

    // POST: api/posts
    // not need login at this stage , add JWT later
    [HttpPost]
    public async Task<ActionResult<Post>> Create(Post post)
    {
        post.CreatedAt = DateTime.UtcNow;
        _db.Posts.Add(post);
        await _db.SaveChangesAsync();

        // 返回 201 + 文章内容
        return CreatedAtAction(nameof(GetBySlug), new { slug = post.Slug }, post);
    }

    // PUT: api/posts/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Post update)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post == null) return NotFound();

        post.Title = update.Title;
        post.Slug = update.Slug;
        post.Summary = update.Summary;
        post.Content = update.Content;
        post.Published = update.Published;
        post.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/posts/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post == null) return NotFound();

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        return NoContent();
    }
    
}