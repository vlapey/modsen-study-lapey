using Services;
using Services.Interfaces;

namespace WebApi.Startup;

public static class ServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }
}