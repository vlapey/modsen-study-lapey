using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace modsen_study_lapey.Startup;

public static class FluentValidationExtension
{
    public static void ConfigureFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        services.AddFluentValidationAutoValidation();
    }
}