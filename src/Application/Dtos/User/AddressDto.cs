namespace Application.DTO.User;

public class AddressDto
{
    public string Street { get; set; }
    public string Suite { get; set; }
    public string City { get; set; }
    public string Zipcode { get; set; }
    public string Name { get; set; }
    public GeoDto? Geo { get; set; }
}
