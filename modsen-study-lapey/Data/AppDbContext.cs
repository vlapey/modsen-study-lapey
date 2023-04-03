using Microsoft.EntityFrameworkCore;
using modsen_study_lapey.Models;

namespace modsen_study_lapey.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var server = "localhost";
        var port = "5432";
        var name = "postgres";
        var user = "admin";
        var pass = "123";

        optionsBuilder.UseNpgsql($"Host={server};Port={port};Database={name};" +
                                 $"Username={user};Password={pass}");
    } 
}