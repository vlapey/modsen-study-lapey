using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Repositories;
using Services.Interfaces.Persistence;

namespace ApplicationBuilder.Startup;

public static class PersistenceExtension
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IBookRepository, BookRepository>();
    }
    
    public static async Task Migrate(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        await using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        await context?.Database.MigrateAsync()!;
    }
}