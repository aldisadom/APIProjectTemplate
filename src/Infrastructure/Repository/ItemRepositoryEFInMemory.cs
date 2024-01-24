using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repository;

public class ItemRepositoryEFInMemory : DbContext, IItemRepository
{
    private readonly DataContext _dataContext;
    public ItemRepositoryEFInMemory(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ItemEntity?> Get(Guid id)
    {
        return await _dataContext.Items.SingleAsync(t => t.Id == id && !t.IsDeleted);
    }

    public async Task<IEnumerable<ItemEntity>> Get()
    {
        return await _dataContext.Items.Where(t => !t.IsDeleted).ToListAsync();
    }

    public async Task<Guid> Add(ItemEntity item)
    {
        await _dataContext.Items.AddAsync(item);
        await _dataContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int> Update(ItemEntity item)
    {
        ItemEntity? editItem = await _dataContext.Items.FindAsync(item.Id);

        if (editItem == null)
            return 0;

        editItem.Name = item.Name;
        editItem.Price = item.Price;
        editItem.ShopId = item.ShopId;

        return await _dataContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        ItemEntity? editItem = await _dataContext.Items.FindAsync(id);

        if (editItem == null)
            return;

        editItem.IsDeleted = true;

        await _dataContext.SaveChangesAsync();
    }

    public async Task AddToBought(BuyItemEntity buy)
    {
        throw new NotImplementedException();
    }
}
