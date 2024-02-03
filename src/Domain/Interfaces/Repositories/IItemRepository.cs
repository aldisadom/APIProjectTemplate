using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IItemRepository
{
    public Task<ItemEntity?> Get(Guid id);

    public Task<IEnumerable<ItemEntity>> Get();

    public Task<Guid> Add(ItemEntity item);

    public Task<int> Update(ItemEntity item);

    public Task Delete(Guid id);
}