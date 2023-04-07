using Microsoft.EntityFrameworkCore;
using Entities.Models;

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
        var dbname = "postgres";
        var user = "admin";
        var pass = "123";

        optionsBuilder.UseNpgsql($"Host={server};Port={port};Database={dbname};" +
                                 $"Username={user};Password={pass}");
    } 
}