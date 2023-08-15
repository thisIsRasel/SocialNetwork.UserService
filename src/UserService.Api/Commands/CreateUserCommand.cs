using MediatR;

namespace UserService.Api.Commands;

public class CreateUserCommand : IRequest
{
    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!;
}
