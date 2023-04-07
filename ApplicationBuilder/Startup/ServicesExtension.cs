using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;

namespace ApplicationBuilder.Startup;

public static class ServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }
}