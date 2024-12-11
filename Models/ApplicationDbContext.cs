using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Area> Areas { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
}