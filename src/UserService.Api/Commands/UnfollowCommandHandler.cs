using MediatR;
using UserService.Domain.Aggregates.FolloweeAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Api.Commands;

public class UnfollowCommandHandler : IRequestHandler<UnfollowCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFolloweeRepository _followeeRepository;

    public UnfollowCommandHandler(
        IUnitOfWork unitOfWork,
        IFolloweeRepository followeeRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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
        await _unitOfWork.SaveChangesAsync();
    }
}
