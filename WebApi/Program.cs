using System.Reflection;
using WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureServices();
builder.Services.ConfigurePersistence(configuration);

builder.Services.ConfigureJwt(configuration);

var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);

builder.Services.ConfigureFluentValidation(assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Migrate();

app.Run();