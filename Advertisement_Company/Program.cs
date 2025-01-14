using Advertisement_Company.DbContexts;
using Advertisement_Company.Services;
using Advertisement_Company;
using NLog;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container
builder.Services.AddDbContext<SqliteDbContext>();

builder.Services.AddControllers();
// For Advertisement.Company
builder.Services.AddHostedService<KafkaConsumerService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
