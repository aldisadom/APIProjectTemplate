using Application.DTO.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserClientService _userService;

    public UserController(UserClientService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.Get());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _userService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserAddDto item)
    {
        UserDto result = await _userService.Add(item);
        return CreatedAtAction(nameof(Get), new { result.Id }, result);
    }

    [HttpPost("{id}/buy")]
    public async Task<IActionResult> Buy(int id, BuyDto buy)
    {
        await _userService.Buy(id, buy);
        return Ok();
    }
}