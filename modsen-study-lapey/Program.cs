using System.Reflection;
using modsen_study_lapey.Startup;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices();
builder.Services.ConfigurePersistence(configuration);

var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);

builder.Services.ConfigureFluentValidation(assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();