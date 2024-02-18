using Clients;
using Clients.Clients;
using Domain.Clients;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Clients;

public class OpenExchangeClient : IClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey = string.Empty;

    public OpenExchangeClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _apiKey = configuration["APIKey"]
                ?? throw new ClientAPIException("API key not provided");
    }

    public async Task<ClientDataResponse> Get(DateTime date)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"https://openexchangerates.org/api/historical/{date:yyyy'-'MM'-'dd}.json?app_id={_apiKey}");

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return new ClientDataResponse
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
                Data = null,
            };

        return new ClientDataResponse
        {
            IsSuccessful = true,
            ErrorMessage = null,
            Data = JsonConvert.DeserializeObject<ClientData>(responseBody),
        };
    }
}