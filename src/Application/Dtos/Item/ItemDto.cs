using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Item;

public class ItemDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public Guid? ShopId { get; set; }
}