using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DataContext : DbContext
{
    public DbSet<ItemEntity> Items { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}