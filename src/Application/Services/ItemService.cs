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

    public async Task<ItemResponce> Get(Guid id)
    {
        ItemEntity item = await _itemRepository.Get(id) ?? throw new NotFoundException("Item not found in DB");

        ItemResponce itemDto = new()
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            ShopId = item.ShopId,
        };

        return itemDto;
    }

    public async Task<List<ItemResponce>> Get()
    {
        List<ItemResponce> items = [];
        IEnumerable<ItemEntity> itemEntities = await _itemRepository.Get();

        if (!itemEntities.Any())
            return [];

        items = itemEntities.Select(i => new ItemResponce()
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
        await Get(id);

        ItemEntity itemEntity = new()
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            ShopId = item.ShopId,
        };

        int result = await _itemRepository.Update(itemEntity);

        if (result > 1)
            throw new InvalidOperationException("Update was performed on multiple rows");
    }

    public async Task Delete(Guid id)
    {
        await Get(id);

        await _itemRepository.Delete(id);
    }

    public decimal GetItemsPrice(ItemResponce item, uint quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Amount must be more than 0");

        decimal netAmount;
        if (quantity > 20)
            netAmount = 0.8m;
        else if (quantity > 10)
            netAmount = 0.9m;
        else
            netAmount = 1.0m;

        return quantity * item.Price * netAmount;
    }

    public async Task AddToShop(Guid id, Guid shopId)
    {
        var itemTask = Get(id);

        ItemResponce item = await itemTask;

        ItemEntity itemEntity = new()
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            ShopId = shopId,
        };

        int result = await _itemRepository.Update(itemEntity);

        if (result > 1)
            throw new InvalidOperationException("Update was performed on multiple rows");
    }
}