using MediatR;
using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Api.Commands;

public class FollowCommandHandler : IRequestHandler<FollowCommand>
{
    private readonly IUserRepository _userRepository;

    public FollowCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task Handle(
        FollowCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId)
            ?? throw new InvalidOperationException("Not a valid user");

        var followee = await _userRepository.GetAsync(request.FolloweeUserId)
            ?? throw new InvalidOperationException("Not a valid followee");

        user.Follow(followee.Id);
        _userRepository.Update(user);

        await _userRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
