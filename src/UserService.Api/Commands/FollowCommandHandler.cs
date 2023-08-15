using MediatR;
using UserService.Domain.Aggregates.FolloweeAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Api.Commands;

public class FollowCommandHandler : IRequestHandler<FollowCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFolloweeRepository _followeeRepository;

    public FollowCommandHandler(
        IUnitOfWork unitOfWork, 
        IFolloweeRepository followeeRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _followeeRepository = followeeRepository ?? throw new ArgumentNullException(nameof(followeeRepository));
    }

    public async Task Handle(
        FollowCommand request, 
        CancellationToken cancellationToken)
    {
        var followee = await Followee.Follow(
            request.UserId,
            request.FolloweeUserId,
            _followeeRepository);

        _followeeRepository.Add(followee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
