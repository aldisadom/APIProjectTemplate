using Application.Interfaces;
using Clients;
using Domain.Clients;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class ClientService : IClientService
{
    private readonly IClient _client;

    public ClientService(IClient client)
    {
        _client = client;
    }

    public async Task<ClientEntity> GetRates(DateTime date)
    {
        ClientDataResponse response = await _client.Get(date);

        if (!response.IsSuccessful || response.Data == null)
            throw new ClientAPIException($"Failed to get {date} exchange rates from external API");

        ClientEntity rates = new();

        if (!long.TryParse(response.Data.Timestamp, out long unixTimeInSeconds))
            throw new ClientAPIException($"Failed to parse timestamp ({response.Data.Timestamp}) from external API");

        rates.Date = DateTimeOffset.FromUnixTimeSeconds(unixTimeInSeconds).UtcDateTime;
        rates.Rates = response.Data.Rates;

        return rates;
    }
}