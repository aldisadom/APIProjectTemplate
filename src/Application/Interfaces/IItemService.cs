using Contracts.Requests;
using Contracts.Responses;

namespace Application.Interfaces;

public interface IItemService
{
    Task<Guid> Add(ItemAddRequest item);
    Task Delete(Guid id);
    Task<ItemListResponse> Get();
    Task<ItemResponse> Get(Guid id);
    Task Update(Guid id, ItemAddRequest item);
}