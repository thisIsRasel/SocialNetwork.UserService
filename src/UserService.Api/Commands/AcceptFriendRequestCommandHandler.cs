using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Api.Commands;

public class AcceptFriendRequestCommandHandler
    : IRequestHandler<AcceptFriendRequestCommand>
{
    private readonly IFriendRequestRepository _friendRequestRepository;

    public AcceptFriendRequestCommandHandler(
        IFriendRequestRepository friendRequestRepository)
    {
        _friendRequestRepository = friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
    }

    public async Task Handle(
        AcceptFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var friendRequest = await _friendRequestRepository
            .GetPendingRequestAsync(request.UserId, request.FriendUserId)
            ?? throw new InvalidOperationException("Friendship does not exist");

        friendRequest.AcceptFriendRequest();
        _friendRequestRepository.Update(friendRequest);

        await _friendRequestRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
