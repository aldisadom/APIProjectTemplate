namespace Domain.Entities;

public class BuyItemEntity : BaseEntity
{
    public Guid ShopId { get; set; }
    public int UserId { get; set; }
    public Guid ItemId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
