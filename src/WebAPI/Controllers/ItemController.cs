using Application.DTO.Item;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a sample controller for demonstrating XML comments in ASP.NET Core.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class ItemController : ControllerBase
{
    private readonly ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Gets all items
    /// </summary>
    /// <returns>list of items</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _itemService.Get());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _itemService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(ItemAddDto item)
    {
        Guid guid = await _itemService.Add(item);
        return CreatedAtAction(nameof(Get), new { Id = guid }, item);
    }
}