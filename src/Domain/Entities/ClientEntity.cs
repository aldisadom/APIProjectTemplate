namespace Domain.Entities;

public class ClientEntity
{
    public DateTime Date { get; set; }
    public Dictionary<string, decimal> Rates { get; set; } = [];
}