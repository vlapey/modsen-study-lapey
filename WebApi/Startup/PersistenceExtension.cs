using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using Services.Interfaces.Persistence;

namespace WebApi.Startup;

public static class PersistenceExtension
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IBookRepository, BookRepository>();
    }
}