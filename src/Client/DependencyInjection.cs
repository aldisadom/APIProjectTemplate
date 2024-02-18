using Infrastructure.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Clients;

public static class DependencyInjection
{
    public static void AddClients(this IServiceCollection services)
    {
        //inject client
        services.AddScoped<IClient, OpenExchangeClient>();
    }
}