using Application.DTO.Item;

namespace Application.Interfaces;

public interface IItemService
{
    Task<Guid> Add(ItemAddDto item);
    Task AddToShop(Guid id, Guid shopId);
    Task Delete(Guid id);
    Task<List<ItemDto>> Get();
    Task<ItemDto> Get(Guid id);
    decimal GetItemsPrice(ItemDto item, uint quantity);
    Task Update(Guid id, ItemAddDto item);
}