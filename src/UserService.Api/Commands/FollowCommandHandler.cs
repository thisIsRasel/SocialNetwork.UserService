using MediatR;
using UserService.Domain.Aggregates.FolloweeAggregate;

namespace UserService.Api.Commands;

public class FollowCommandHandler : IRequestHandler<FollowCommand>
{
    private readonly IFolloweeRepository _followeeRepository;

    public FollowCommandHandler(
        IFolloweeRepository followeeRepository)
    {
        _followeeRepository = followeeRepository ?? throw new ArgumentNullException(nameof(followeeRepository));
    }

    public async Task Handle(
        FollowCommand request,
        CancellationToken cancellationToken)
    {
        await Followee.Follow(
            request.UserId,
            request.FolloweeUserId,
            _followeeRepository);

        await _followeeRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
