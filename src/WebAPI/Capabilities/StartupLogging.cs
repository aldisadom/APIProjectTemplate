using Serilog;

namespace WebAPI.Capabilities;

public static class StartupLogging
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services, IConfiguration configuration)
    {
        //change logger
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        return services;
    }
}
