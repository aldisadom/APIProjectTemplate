using Infrastructure.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Clients;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string? dbConnectionString)
    {
        //inject client
        services.AddScoped<IClient, Clienta>();
    }
}