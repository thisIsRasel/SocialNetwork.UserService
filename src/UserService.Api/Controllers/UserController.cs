using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Commands;

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
}
