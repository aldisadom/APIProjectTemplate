using Application.DTO.Shop;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ShopService
{
    private readonly IShopRepository _iShopRepository;

    public ShopService(IShopRepository iShopRepository)
    {
        _iShopRepository = iShopRepository;
    }

    public async Task<ShopDto> Get(Guid id)
    {
        ShopEntity shop = await _iShopRepository.Get(id) ?? throw new NotFoundException("Item not found in DB");

        ShopDto shopDto = new()
        {
            Id = id,
            Name = shop.Name,
            Address = shop.Address,
        };

        return shopDto;
    }

    public async Task<List<ShopDto>> Get()
    {
        List<ShopDto> shops = [];
        IEnumerable<ShopEntity> shopEntities = await _iShopRepository.Get();

        if (!shopEntities.Any())
            return [];

        shops = shopEntities.Select(s => new ShopDto()
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
        }).ToList();

        return shops;
    }

    public async Task<Guid> Add(ShopAddDto shop)
    {
        ShopEntity shopEntity = new()
        {
            Name = shop.Name,
            Address = shop.Address,
        };

        return await _iShopRepository.Add(shopEntity);
    }

    public async Task Update(Guid id, ShopAddDto item)
    {
        await Get(id);

        ShopEntity shopEntity = new()
        {
            Id = id,
            Name = item.Name,
            Address = item.Address,
        };

        int result = await _iShopRepository.Update(shopEntity);

        if (result > 1)
            throw new InvalidOperationException("Update was performed on multiple rows");
    }

    public async Task Delete(Guid id)
    {
        await Get(id);

        await _iShopRepository.Delete(id);
    }
}
