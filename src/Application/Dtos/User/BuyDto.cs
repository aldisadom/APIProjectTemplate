using System.ComponentModel.DataAnnotations;

namespace Application.DTO.User;

public class BuyDto
{
    [Required]
    public Guid ShopId { get; set; }

    [Required]
    public Guid ItemId { get; set; }

    [Required]
    public uint Quantity { get; set; }
}
