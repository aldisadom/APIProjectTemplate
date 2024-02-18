using Domain.Clients;

namespace Clients;

public interface IClient
{
    Task<ClientDataResponse> Get(DateTime date);
}