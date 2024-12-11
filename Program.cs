using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql("Host=localhost;Port=5432;Database=test_dotnet_db;Username=test_dotnet_api_user;Password=;"));

// Logging
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register all IItemHandler implementations
var handlerType = typeof(IItemHandler);
var handlers = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => handlerType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
foreach (var handler in handlers)
{
    builder.Services.AddTransient(handlerType, handler);
}

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();