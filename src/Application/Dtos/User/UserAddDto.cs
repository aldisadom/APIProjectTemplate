namespace Application.DTO.User;

public class UserAddDto
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public AddressDto? Address { get; set; }
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public CompanyDto? Company { get; set; }
}
