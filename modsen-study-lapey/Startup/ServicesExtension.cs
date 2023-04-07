using Services;
using Services.Interfaces;

namespace modsen_study_lapey.Startup;

public static class ServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
    }
}