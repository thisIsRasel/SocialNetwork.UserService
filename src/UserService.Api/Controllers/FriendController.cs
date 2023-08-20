using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Commands;

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

    [HttpPost("{friendUserId}/SendFriendRequest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SendFriendRequestAsync(
        [FromRoute] string friendUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new SendFriendRequestCommand(
            userId: userId,
            friendUserId: friendUserId));

        return Ok();
    }

    [HttpPost("{friendUserId}/AcceptFriendRequest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AcceptFriendRequestAsync(
        [FromRoute] string friendUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new AcceptFriendRequestCommand(
            userId: userId,
            friendUserId: friendUserId));

        return Ok();
    }

    [HttpPost("{friendUserId}/RejectFriendRequest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RejectFriendRequestAsync(
        [FromRoute] string friendUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new RejectFriendRequestCommand(
            userId: userId,
            friendUserId: friendUserId));

        return Ok();
    }
}
