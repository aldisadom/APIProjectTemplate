using Domain.Entities;

namespace Application.Interfaces;

public interface IClientService
{
    Task<ClientEntity> GetRates(DateTime date);
}