using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ItemResponse> Get(Guid id)
    {
        ItemEntity itemEntity = await _itemRepository.Get(id) 
            ?? throw new NotFoundException("Item not found in DB");

        ItemResponse itemResponse = new()
        {
            Id = id,
            Name = itemEntity.Name,
            Price = itemEntity.Price,
            ShopId = itemEntity.ShopId,
        };

        return itemResponse;
    }

    public async Task<ItemListResponse> Get()
    {
        
        IEnumerable<ItemEntity> itemEntities = await _itemRepository.Get();

        ItemListResponse items = new ();
        if (!itemEntities.Any())
            return items;

        items.Items = itemEntities.Select(i => new ItemResponse()
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price,
            ShopId = i.ShopId,
        }).ToList();

        return items;
    }

    public async Task<Guid> Add(ItemAddRequest item)
    {
        ItemEntity itemEntity = new()
        {
            Name = item.Name,
            Price = item.Price,
        };

        return await _itemRepository.Add(itemEntity);
    }

    public async Task Update(Guid id, ItemAddRequest item)
    {
        ItemEntity itemEntity = await _itemRepository.Get(id)
            ?? throw new NotFoundException("Item not found in DB");

        itemEntity = new ItemEntity()
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            ShopId = item.ShopId,
        };

        await _itemRepository.Update(itemEntity);
    }

    public async Task Delete(Guid id)
    {
        ItemEntity itemEntity = await _itemRepository.Get(id)
            ?? throw new NotFoundException("Item not found in DB");

        await _itemRepository.Delete(id);
    }
}