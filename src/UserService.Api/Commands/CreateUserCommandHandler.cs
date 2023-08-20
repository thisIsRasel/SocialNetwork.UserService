using MediatR;
using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Api.Commands;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await User.CreateAsync(
            name: request.Name,
            email: request.Email,
            userRepository: _userRepository);

        _userRepository.Add(user);

        await _userRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
