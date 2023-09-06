using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Reflection;
using WeatherStation.Entities;
using WeatherStation.Helpers;
using WeatherStation.Middleware;
using WeatherStation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<CustomDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IMeasurmentService, MeasurmentService>();
builder.Services.AddScoped<InitDatabse>();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Front",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .WithExposedHeaders("Location");
        }
    );
});

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<InitDatabse>();
seeder.init();

app.UseCors("Front");

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Measurment");
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
