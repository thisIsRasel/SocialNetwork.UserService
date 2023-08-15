using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Commands;

namespace UserService.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FolloweeController : ControllerBase
{
    private readonly IMediator _mediator;

    public FolloweeController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("{followeeUserId}/Follow")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> FollowAsync([FromRoute] string followeeUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new FollowCommand(
            userId: userId,
            followeeUserId: followeeUserId));

        return Ok();
    }

    [HttpPost("{followeeUserId}/Unfollow")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UnfollowAsync([FromRoute] string followeeUserId)
    {
        var userId = User.FindFirstValue("userId");
        if (string.IsNullOrWhiteSpace(userId))
        {
            return BadRequest();
        }

        await _mediator.Send(new UnfollowCommand(
            userId: userId,
            followeeUserId: followeeUserId));

        return Ok();
    }
}
