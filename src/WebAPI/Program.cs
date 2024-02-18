using Application;
using Clients;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;
using WebAPI.Middleware;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddApplication();
        builder.Services.AddClients();


        string dbConnectionString = builder.Configuration.GetConnectionString("PostgreConnection")
            ?? throw new ArgumentNullException("Postgre connection string not found");
        builder.Services.AddInfrastructure(dbConnectionString);

        //change logger
        builder.Logging.ClearProviders();
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Logging.AddSerilog(logger);

        builder.Services.AddHttpClient();

        var app = builder.Build();

        //custom middleware
        app.UseMiddleware<ErrorChecking>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
/*
 *     public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services
            .ConfigureInjection(builder.Configuration)
            .ConfigureLogging(builder.Configuration)
            .ConfigureSwagger();

        builder.Host.UseSerilog();

        var app = builder.Build();

        //custom error handling middleware        
        app.UseMiddleware<ErrorChecking>();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseSwaggerWithUI();

        app.Run();
    }
 */ 