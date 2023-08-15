using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Commands;
using UserService.Api.Queries.GetFriends;

namespace UserService.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendController : ControllerBase
{
    private readonly IMediator _mediator;

    public FriendController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
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

    [HttpPost("{friendUserId}/Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAsync([FromRoute] string friendUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new AddFriendshipCommand(
            userId: userId,
            friendUserId: friendUserId));

        return Ok();
    }

    [HttpPost("{friendUserId}/Accept")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AcceptAsync([FromRoute] string friendUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new AcceptFriendshipCommand(
            userId: userId,
            friendUserId: friendUserId));

        return Ok();
    }

    [HttpPost("{friendUserId}/Remove")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveAsync([FromRoute] string friendUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new RemoveFriendshipCommand(
            userId: userId,
            friendUserId: friendUserId));

        return Ok();
    }
}
