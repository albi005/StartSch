using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StartSch.Data;

public class Db(DbContextOptions<Db> options) : DbContext(options)
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many#unidirectional-many-to-many
        // modelBuilder.Entity<Post>()
        //     .HasMany(p => p.Tags)
        //     .WithMany();
    }
}

public record Tag(string Path)
{
    public int Id { get; set; }

    public List<Post> Posts { get; set; } = [];
    public List<Opening> Openings { get; set; } = [];
    public List<Event> Events { get; set; } = [];
}

public class Post
{
    public int Id { get; set; }
    [MaxLength(50)] public required string Title { get; set; }
    [MaxLength(300)] public string? Excerpt { get; set; }
    [MaxLength(2000)] public string? Body { get; set; }
    [MaxLength(500)] public string? Url { get; set; }
    public DateTime PublishedAtUtc { get; set; }

    public List<Tag> Tags { get; set; } = [];
}

public class Group
{
    public int Id { get; set; }
    public List<Post> Posts { get; set; } = [];
    public List<Opening> Openings { get; set; } = [];
}

public class Opening
{
    public int Id { get; set; }
    public Group Group { get; set; } = null!;
    public List<Post> Posts { get; set; } = [];
}

public class Event
{
    public int Id { get; set; }
    public Group? Group { get; set; }
    public List<Post> Posts { get; set; } = [];
}