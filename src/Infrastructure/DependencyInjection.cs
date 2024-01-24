using Domain.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Contexts;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string? dbConnectionString)
    {
        services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(dbConnectionString));

        //inject DataContext
        //        services.AddDbContext<DataContext>(sp => sp.UseInMemoryDatabase("MyDatabase"));
        services.AddDbContext<DataContext>(sp => sp.UseNpgsql(new NpgsqlConnection(dbConnectionString)));

        //inject Repository
        //        builder.Services.AddScoped<IItemRepository, ItemRepositoryEFInMemory>();
        services.AddScoped<IItemRepository, ItemRepositoryPostgre>();
        services.AddScoped<IShopRepository, ShopRepository>();

        //inject client
        services.AddScoped<IGeneralClient, UserClient>();
    }
}
