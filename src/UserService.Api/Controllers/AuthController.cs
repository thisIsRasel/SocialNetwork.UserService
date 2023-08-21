using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Queries.GetToken;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Token")]
    public async Task<IActionResult> GetToken([FromForm] string email)
    {
        var result = await _mediator.Send(new GetTokenQuery(email));

        return Ok(result);
    }
}
