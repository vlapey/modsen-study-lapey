using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationBuilder.Startup;

public static class WebApiExtension
{
    public static void ConfigureWebApiServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddControllers()
            .PartManager
            .ApplicationParts
            .Add(new AssemblyPart(assembly));
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void ConfigureWebApiApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}