namespace Domain.Entities;

public class ItemEntity : BaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public Guid? ShopId { get; set; }
}