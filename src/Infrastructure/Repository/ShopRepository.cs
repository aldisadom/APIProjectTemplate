using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;

namespace Infrastructure.Repository;

public class ShopRepository : IShopRepository
{
    private readonly IDbConnection _dbConnection;
    public ShopRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ShopEntity?> Get(Guid id)
    {
        var queryArguments = new
        {
            Id = id
        };
        return await _dbConnection.QuerySingleAsync<ShopEntity>("SELECT * FROM shops" +
                                                " WHERE id=@Id AND \"isDeleted\"=false", queryArguments);
    }

    public async Task<IEnumerable<ShopEntity>> Get()
    {
        return await _dbConnection.QueryAsync<ShopEntity>("SELECT * FROM shops" +
                                                " WHERE \"isDeleted\"=false");
    }

    public async Task<Guid> Add(ShopEntity item)
    {
        string sql = $"INSERT INTO shops" +
                        " (name, address)" +
                        " VALUES (@Name, @Address)" +
                        "RETURNING id";

        return await _dbConnection.ExecuteScalarAsync<Guid>(sql, item);
    }

    public async Task<int> Update(ShopEntity item)
    {
        return await _dbConnection.ExecuteAsync("UPDATE shops" +
                                        " SET name=@Name,address=@Address" +
                                        " WHERE id=@Id AND \"isDeleted\"=false", item);
    }

    public async Task Delete(Guid id)
    {
        var queryArguments = new
        {
            Id = id
        };
        await _dbConnection.ExecuteAsync("UPDATE shops" +
                                        " SET \"isDeleted\"=true" +
                                        " WHERE id=@Id AND \"isDeleted\"=false", queryArguments);
    }
}
