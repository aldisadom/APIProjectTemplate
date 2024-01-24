using Application.DTO.Item;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ShopService _shopService;

    public ItemService(IItemRepository itemRepository, ShopService shopService)
    {
        _itemRepository = itemRepository;
        _shopService = shopService;
    }

    public async Task<ItemDto> Get(Guid id)
    {
        ItemEntity item = await _itemRepository.Get(id) ?? throw new NotFoundException("Item not found in DB");

        ItemDto itemDto = new()
        {
            Id = id,
            Name = item.Name,
            Price = item.Price,
            ShopId = item.ShopId,
        };

        return itemDto;
    }

    public async Task<List<ItemDto>> Get()
    {
        List<ItemDto> items = [];
        IEnumerable<ItemEntity> itemEntities = await _itemRepository.Get();

        if (!itemEntities.Any())
            return [];

        items = itemEntities.Select(i => new ItemDto()
        {
            Id = i.Id,
            Name = i.Name,
            Price = i.Price,
            ShopId = i.ShopId,
        }).ToList();

        return items;
    }

    public async Task<Guid> Add(ItemAddDto item)
    {
        ItemEntity itemEntity = new()
        {
            Name = item.Name,
            Price = item.Price,
        };

        return await _itemRepository.Add(itemEntity);
    }

    public async Task Update(Guid id, ItemAddDto item)
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

    public decimal GetItemsPrice(ItemDto item, uint quantity)
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

    public async Task AddToBought(BuyItemEntity buy)
    {
        await _itemRepository.AddToBought(buy);
    }

    public async Task AddToShop(Guid id, Guid shopId)
    {
        var itemTask = Get(id);
        var shopTask = _shopService.Get(shopId);

        ItemDto item = await itemTask;
        await shopTask;

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
