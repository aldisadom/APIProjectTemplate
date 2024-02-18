using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a OpenExchangeRates Client example how to get rates for date
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clientService"></param>
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Get rates for selected day
    /// </summary>
    /// <param name="date">Rates day</param>
    /// <returns></returns>
    [HttpGet("{date}")]
    public async Task<IActionResult> Get(DateTime date)
    {
        return Ok(await _clientService.GetRates(date));
    }
}