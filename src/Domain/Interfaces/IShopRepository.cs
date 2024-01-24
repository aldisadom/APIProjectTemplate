using Domain.Entities;

namespace Domain.Interfaces;

public interface IShopRepository
{
    public Task<ShopEntity?> Get(Guid id);

    public Task<IEnumerable<ShopEntity>> Get();

    public Task<Guid> Add(ShopEntity item);

    public Task<int> Update(ShopEntity item);

    public Task Delete(Guid id);
}
