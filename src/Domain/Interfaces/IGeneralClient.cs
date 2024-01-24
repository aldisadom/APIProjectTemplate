using Domain.Dto;

namespace Domain.Interfaces;

public interface IGeneralClient
{
    public Task<JsonPlaceholderResult<T>> Get<T>(string endpoint, int id) where T : class;

    public Task<JsonPlaceholderResult<T>> Get<T>(string endpoint) where T : class;

    public Task<JsonPlaceholderResult<T1>> Add<T, T1>(string endpoint, T data) where T : class where T1 : class;

    public Task<JsonPlaceholderResult> Update<T>(string endpoint, T data);
}
