using Clients.Clients;

namespace Domain.Clients;

public class ClientDataResponse
{
    public bool IsSuccessful { get; set; }
    public string? ErrorMessage { get; set; }
    public ClientData? Data { get; set; }
}