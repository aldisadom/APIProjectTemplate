using Application.DTO.Item;
using Application.DTO.User;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class UserClientService
{
    private readonly IGeneralClient _user;
    private readonly ItemService _itemService;

    public UserClientService(IGeneralClient generalExternalAPI, ItemService itemService)
    {
        _user = generalExternalAPI;
        _itemService = itemService;
    }

    public async Task<UserDto> Get(int id)
    {
        var result = await _user.Get<UserDto>("users", id);

        if (!result.IsSuccessful)
            throw new NotFoundException("User not found");

        return result.Data!;
    }

    public async Task<List<UserDto>> Get()
    {
        var result = await _user.Get<IEnumerable<UserDto>>("users");

        if (!result.IsSuccessful)
            throw new NotFoundException("User not found");

        return result.Data!.ToList();
    }

    public async Task<UserDto> Add(UserAddDto item)
    {
        var result = await _user.Add<UserAddDto, UserDto>("users", item);

        if (!result.IsSuccessful)
            throw new NotFoundException("User add failed");

        return result.Data!;
    }

    public async Task Buy(int id, BuyDto buy)
    {
        var userTask = Get(id);
        var itemTask = _itemService.Get(buy.ItemId);

        await userTask;
        ItemDto item = await itemTask;

        if (item.ShopId != buy.ShopId)
            throw new NotFoundException("Item not found in this shop");

        BuyItemEntity buyEntity = new()
        {
            ItemId = buy.ItemId,
            UserId = id,
            ShopId = buy.ShopId,
            Price = _itemService.GetItemsPrice(item, buy.Quantity),
            Quantity = (int)buy.Quantity,
        };

        await _itemService.AddToBought(buyEntity);
    }
}
