using MediatR;
using UserService.Domain.Aggregates.FriendAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Api.Commands;

public class AcceptFriendshipCommandHandler
    : IRequestHandler<AcceptFriendshipCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFriendRepository _friendRepository;

    public AcceptFriendshipCommandHandler(
        IUnitOfWork unitOfWork,
        IFriendRepository friendRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _friendRepository = friendRepository ?? throw new ArgumentNullException(nameof(friendRepository));
    }

    public async Task Handle(
        AcceptFriendshipCommand request,
        CancellationToken cancellationToken)
    {
        var friend = await _friendRepository
            .GetAsync(request.UserId, request.FriendUserId)
            ?? throw new InvalidOperationException("Friendship does not exist");

        friend.AcceptFriendship();

        _friendRepository.Update(friend);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
