using Domain.Entities;

namespace Domain.Interfaces;

public interface IItemRepository
{
    Task<ItemEntity?> Get(Guid id);
    Task<IEnumerable<ItemEntity>> Get();
    Task<Guid> Add(ItemEntity item);
    Task Update(ItemEntity item);
    Task Delete(Guid id);
}