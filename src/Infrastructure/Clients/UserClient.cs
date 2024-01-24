using Domain.Dto;
using Domain.Interfaces;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Infrastructure.Clients;

public class UserClient : IGeneralClient
{
    private readonly string _url = "https://jsonplaceholder.typicode.com/";
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly bool _withErrorMessage = false;

    public UserClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<JsonPlaceholderResult<T>> Get<T>(string endpoint, int id) where T : class
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"{_url}{endpoint}/{id}");

        var stringContent = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!_withErrorMessage && !response.IsSuccessStatusCode)
            return new JsonPlaceholderResult<T>
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
                Data = null,
            };

        if (!response.IsSuccessStatusCode)
        {
            ErrorViewModel responseError = JsonConvert.DeserializeObject<ErrorViewModel>(responseBody);
            return new JsonPlaceholderResult<T>
            {
                IsSuccessful = false,
                ErrorMessage = responseError.Message,
                Data = null,
            };
        }

        return new JsonPlaceholderResult<T>
        {
            IsSuccessful = true,
            ErrorMessage = null,
            Data = JsonConvert.DeserializeObject<T>(responseBody),
        };
    }

    public async Task<JsonPlaceholderResult<T>> Get<T>(string endpoint) where T : class
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"{_url}{endpoint}");

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = client.Send(request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!_withErrorMessage && !response.IsSuccessStatusCode)
            return new JsonPlaceholderResult<T>
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
                Data = null,
            };

        if (!response.IsSuccessStatusCode)
        {
            ErrorViewModel responseError = JsonConvert.DeserializeObject<ErrorViewModel>(responseBody);
            return new JsonPlaceholderResult<T>
            {
                IsSuccessful = false,
                ErrorMessage = responseError.Message,
                Data = null,
            };
        }

        return new JsonPlaceholderResult<T>
        {
            IsSuccessful = true,
            ErrorMessage = null,
            Data = JsonConvert.DeserializeObject<T>(responseBody),
        };
    }

    public async Task<JsonPlaceholderResult<T1>> Add<T, T1>(string endpoint, T data) where T : class where T1 : class
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"{_url}{endpoint}");

        var response = await client.PostAsJsonAsync(url, data);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!_withErrorMessage && !response.IsSuccessStatusCode)
            return new JsonPlaceholderResult<T1>
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
                Data = null
            };

        if (!response.IsSuccessStatusCode)
        {
            ErrorViewModel responseError = JsonConvert.DeserializeObject<ErrorViewModel>(responseBody);
            return new JsonPlaceholderResult<T1>
            {
                IsSuccessful = false,
                ErrorMessage = responseError.Message,
                Data = null
            };
        }

        return new JsonPlaceholderResult<T1>
        {
            IsSuccessful = true,
            ErrorMessage = null,
            Data = JsonConvert.DeserializeObject<T1>(responseBody)
        };
    }

    public async Task<JsonPlaceholderResult> Update<T>(string endpoint, T data)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"{_url}{endpoint}");

        var response = await client.PutAsJsonAsync(url, data);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!_withErrorMessage && !response.IsSuccessStatusCode)
            return new JsonPlaceholderResult
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString(),
            };

        if (!response.IsSuccessStatusCode)
        {
            ErrorViewModel responseError = JsonConvert.DeserializeObject<ErrorViewModel>(responseBody);
            return new JsonPlaceholderResult
            {
                IsSuccessful = false,
                ErrorMessage = responseError.Message,
            };
        }

        return new JsonPlaceholderResult
        {
            IsSuccessful = true,
            ErrorMessage = null,
        };
    }

    public async Task<JsonPlaceholderResult> Delete(string endpoint, int id)
    {
        using HttpClient client = _httpClientFactory.CreateClient();
        var url = new Uri($"{_url}{endpoint}/{id}");

        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        request.Content = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
        var response = client.Send(request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (!_withErrorMessage && !response.IsSuccessStatusCode)
            return new JsonPlaceholderResult
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString()
            };

        if (!response.IsSuccessStatusCode)
        {
            ErrorViewModel responseError = JsonConvert.DeserializeObject<ErrorViewModel>(responseBody);
            return new JsonPlaceholderResult
            {
                IsSuccessful = false,
                ErrorMessage = responseError.Message
            };
        }

        return new JsonPlaceholderResult
        {
            IsSuccessful = true,
            ErrorMessage = null
        };
    }
}
