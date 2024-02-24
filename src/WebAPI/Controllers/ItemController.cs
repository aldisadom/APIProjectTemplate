using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Examples;
using System.Net;
using WebAPI.SwaggerExamples.Item;

namespace WebAPI.Controllers;

/// <summary>
/// This is a item controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Gets all items
    /// </summary>
    /// <returns>list of items</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
    [SwaggerResponseExample((HttpStatusCode)StatusCodes.Status200OK, typeof(ItemAdd))]
    public async Task<IActionResult> Get()
    {
        return Ok(await _itemService.Get());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _itemService.Get(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(ItemAddRequest item)
    {
        Guid id = await _itemService.Add(item);
        return CreatedAtAction(nameof(Add), new { id });
    }
}