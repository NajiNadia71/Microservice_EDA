using Production_Company.DbContexts;
using Production_Company.Services;
using Production_Company.BackgroundServices;
using Microsoft.EntityFrameworkCore;
using NLog;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddDbContext<SqliteDbContext>();

// For Production.Company
builder.Services.AddSingleton<KafkaProducerService>();
builder.Services.AddHostedService<ProductionSchedulerService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
