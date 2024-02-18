using Contracts.Requests;
using Contracts.Responses;

namespace Application.Interfaces;

public interface IItemService
{
    Task<Guid> Add(ItemAddRequest item);
    Task AddToShop(Guid id, Guid shopId);
    Task Delete(Guid id);
    Task<List<ItemResponce>> Get();
    Task<ItemResponce> Get(Guid id);
    decimal GetItemsPrice(ItemResponce item, uint quantity);
    Task Update(Guid id, ItemAddRequest item);
}