using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Shop;

public class ShopAddDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }
}
