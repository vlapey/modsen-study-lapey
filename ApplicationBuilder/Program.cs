using System.Reflection;
using ApplicationBuilder.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var assembly = Assembly.Load("WebApi");

builder.Services.ConfigureWebApiServices(assembly);

builder.Services.ConfigureServices();
builder.Services.ConfigurePersistence(configuration);

builder.Services.AddAutoMapper(assembly);

builder.Services.ConfigureFluentValidation(assembly);

var app = builder.Build();

app.ConfigureWebApiApplication();

await app.Migrate();

app.Run();