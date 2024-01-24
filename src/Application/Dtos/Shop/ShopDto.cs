using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Shop;

public class ShopDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }
}
