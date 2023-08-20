using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Commands;
using UserService.Api.Queries.GetFollowers;
using UserService.Api.Queries.GetFriends;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Create")]
    public async Task CreateAsync([FromBody] CreateUserCommand command)
    {
        await _mediator.Send(command);
    }

    [HttpGet("Friends")]
    public async Task<IActionResult> GetFriendsAsync(
        [FromQuery] string query,
        [FromQuery] int page)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetFriendsQuery(
            userId,
            query: query,
            page: page));

        return Ok(result);
    }

    [HttpGet("Followers")]
    public async Task<IActionResult> GetFollowersAsync(
        [FromQuery] string query,
        [FromQuery] int page)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetFollowersQuery(
            userId,
            query: query,
            page: page));

        return Ok(result);
    }
}
