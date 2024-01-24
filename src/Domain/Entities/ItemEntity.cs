using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("items")]
public class ItemEntity : BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public decimal Price { get; set; }

    [Column("shopId")]
    public Guid? ShopId { get; set; }
}
