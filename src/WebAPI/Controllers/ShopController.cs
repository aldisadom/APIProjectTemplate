using Application.DTO.Shop;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;

    public ShopController(ShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _shopService.Get());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _shopService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(ShopAddDto shop)
    {
        Guid guid = await _shopService.Add(shop);
        return CreatedAtAction(nameof(Get), new { Id = guid }, shop);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ShopAddDto shop)
    {
        await _shopService.Update(id, shop);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _shopService.Delete(id);
        return Ok();
    }
}