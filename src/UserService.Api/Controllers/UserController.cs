using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Commands;
using UserService.Api.Queries.GetFollowers;
using UserService.Api.Queries.GetFollowings;
using UserService.Api.Queries.GetFriends;
using UserService.Api.Queries.GetIncomingFriendRequests;
using UserService.Api.Queries.GetOutgoingFriendRequests;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status200OK)]
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

    [Authorize]
    [HttpGet("Friends")]
    public async Task<IActionResult> GetFriendsAsync(
        [FromQuery] string? query,
        [FromQuery] int page = 1)
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

    [Authorize]
    [HttpGet("Followers")]
    public async Task<IActionResult> GetFollowersAsync(
        [FromQuery] string? query,
        [FromQuery] int page = 1)
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

    [Authorize]
    [HttpGet("Followings")]
    public async Task<IActionResult> GetFollowingsAsync(
        [FromQuery] int page = 1)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetFollowingsQuery(
            userId: userId,
            page: page));

        return Ok(result);
    }

    [Authorize]
    [HttpGet("IncomingFriendRequests")]
    public async Task<IActionResult> GetIncomingFriendRequestsAsync(
        [FromQuery] int page = 1)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetIncomingFriendRequestsQuery(
            userId: userId,
            page: page));

        return Ok(result);
    }

    [Authorize]
    [HttpGet("OutgoingFriendRequests")]
    public async Task<IActionResult> GetOutgoingFriendRequestsAsync(
        [FromQuery] int page = 1)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        var result = await _mediator.Send(new GetOutgoingFriendRequestsQuery(
            userId: userId,
            page: page));

        return Ok(result);
    }
}
