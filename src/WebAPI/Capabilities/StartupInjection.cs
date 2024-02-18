using Application;
using Clients;
using Infrastructure;

namespace WebAPI.Capabilities;

public static class StartupInjection
{
    public static IServiceCollection ConfigureInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddApplication();
        services.AddClients();

        string dbConnectionString = configuration.GetConnectionString("PostgreConnection")
            ?? throw new ArgumentNullException("Postgre connection string not found");

        services.AddInfrastructure(dbConnectionString);

        return services;
    }
}
