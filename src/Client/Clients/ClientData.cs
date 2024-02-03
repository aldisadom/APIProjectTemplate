namespace Clients.Clients;

public class ClientData
{
    public string Disclaimer { get; set; } = string.Empty;
    public string License { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
    public string Base { get; set; } = string.Empty;
    public Dictionary<string, decimal> Rates { get; set; } = [];
}