using MediatR;
using UserService.Domain.Aggregates.FolloweeAggregate;

namespace UserService.Api.Commands;

public class UnfollowCommandHandler : IRequestHandler<UnfollowCommand>
{
    private readonly IFolloweeRepository _followeeRepository;

    public UnfollowCommandHandler(
        IFolloweeRepository followeeRepository)
    {
        _followeeRepository = followeeRepository ?? throw new ArgumentNullException(nameof(followeeRepository));
    }
    public async Task Handle(
        UnfollowCommand request,
        CancellationToken cancellationToken)
    {
        var followee = await _followeeRepository
            .GetAsync(request.UserId, request.FolloweeUserId)
            ?? throw new InvalidOperationException("User is not followed");

        followee.Unfollow();
        _followeeRepository.Update(followee);

        await _followeeRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
