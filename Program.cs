using System.Text.Json.Serialization;

using APICatalog.Context;

using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add health checks
builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options
    .JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? mySqlConnection = builder
    .Configuration
    .GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(mySqlConnection))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");
}

// Read and validate required environment variables
string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
string? dbUser = Environment.GetEnvironmentVariable("DB_USER");
string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
string? dbPort = Environment.GetEnvironmentVariable("DB_PORT");

if (string.IsNullOrEmpty(dbHost))
    throw new InvalidOperationException(
        "Environment variable 'DB_HOST' is not set.");
if (string.IsNullOrEmpty(dbName))
    throw new InvalidOperationException(
        "Environment variable 'DB_NAME' is not set.");
if (string.IsNullOrEmpty(dbUser))
    throw new InvalidOperationException(
        "Environment variable 'DB_USER' is not set.");
if (string.IsNullOrEmpty(dbPassword))
    throw new InvalidOperationException(
        "Environment variable 'DB_PASSWORD' is not set.");
if (string.IsNullOrEmpty(dbPort))
    throw new InvalidOperationException(
        "Environment variable 'DB_PORT' is not set.");

mySqlConnection = mySqlConnection
    .Replace("${DB_HOST}", dbHost)
    .Replace("${DB_NAME}", dbName)
    .Replace("${DB_USER}", dbUser)
    .Replace("${DB_PASSWORD}", dbPassword)
    .Replace("${DB_PORT}", dbPort);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        mySqlConnection,
        ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Protect healthcheck endpoint with token in header
var healthToken = Environment.GetEnvironmentVariable("HEALTHCHECK_TOKEN");

app.UseWhen(ctx => ctx.Request.Path.Equals("/healthcheck", StringComparison.OrdinalIgnoreCase), branch =>
    branch.Use(async (ctx, next) =>
        {
            // Block if token is not configured in the environment
            if (string.IsNullOrEmpty(healthToken))
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await ctx.Response.WriteAsync("Unauthorized: token not configured");
                return;
            }

            // Block if the header is not sent or is different
            var hasHeader = ctx.Request.Headers.TryGetValue("X-Health-Token", out var provided);
            if (!hasHeader || !string.Equals(provided.ToString(), healthToken, StringComparison.Ordinal))
            {
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await ctx.Response.WriteAsync("Unauthorized");
                return;
            }

            await next();
        }));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthcheck");

app.Run();