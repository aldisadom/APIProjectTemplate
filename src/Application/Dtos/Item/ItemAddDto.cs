using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Item;

public class ItemAddDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    public Guid? ShopId { get; set; }
}