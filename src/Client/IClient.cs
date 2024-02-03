using Domain.Clients;

namespace Clients;

public interface IClient
{
    public Task<ClientDataResponse> Get(DateTime date);
}