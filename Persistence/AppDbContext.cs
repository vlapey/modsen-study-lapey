using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext : IdentityUserContext<IdentityUser>
{
    public DbSet<Book> Books { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().Property(p => p.Name).HasMaxLength(20);
        modelBuilder.Entity<Book>().Property(p => p.Author).HasMaxLength(20);
        base.OnModelCreating(modelBuilder);
    }
}