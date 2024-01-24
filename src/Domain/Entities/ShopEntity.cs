using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ShopEntity : BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("address")]
    public string Address { get; set; }
}
