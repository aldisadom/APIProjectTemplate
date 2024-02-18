using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// This is a sample controller for demonstrating XML comments in ASP.NET Core.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status500InternalServerError)]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly ILogger<ItemController> _logger;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="itemService"></param>
    /// <param name="logger"></param>
    public ItemController(IItemService itemService, ILogger<ItemController> logger)
    {
        _itemService = itemService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all items
    /// </summary>
    /// <returns>list of items</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ItemResponce), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _itemService.Get());
    }

    /// <summary>
    /// Get single item
    /// </summary>
    /// <param name="id">Item ID</param>
    /// <returns>Item properties</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ItemResponce), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponce), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _itemService.Get(id));
    }

    /// <summary>
    /// Adds item to shop
    /// </summary>
    /// <param name="item">Item data to add</param>
    /// <returns>ID of item</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(ItemAddRequest item)
    {
        Guid id = await _itemService.Add(item);
        return CreatedAtAction(nameof(Get), new { id });
    }
}