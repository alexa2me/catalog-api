using APICatalog.Context;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(mySqlConnection))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Read and validate required environment variables
string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
string? dbUser = Environment.GetEnvironmentVariable("DB_USER");
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

if (string.IsNullOrEmpty(dbHost))
    throw new InvalidOperationException("Environment variable 'DB_HOST' is not set.");
if (string.IsNullOrEmpty(dbName))
    throw new InvalidOperationException("Environment variable 'DB_NAME' is not set.");
if (string.IsNullOrEmpty(dbUser))
    throw new InvalidOperationException("Environment variable 'DB_USER' is not set.");
if (string.IsNullOrEmpty(dbPassword))
    throw new InvalidOperationException("Environment variable 'DB_PASSWORD' is not set.");

mySqlConnection = mySqlConnection
    .Replace("${DB_HOST}", dbHost)
    .Replace("${DB_NAME}", dbName)
    .Replace("${DB_USER}", dbUser)
    .Replace("${DB_PASSWORD}", dbPassword);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
