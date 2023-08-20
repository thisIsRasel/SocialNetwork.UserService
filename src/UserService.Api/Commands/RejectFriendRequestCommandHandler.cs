using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Api.Commands;

public sealed class RejectFriendRequestCommandHandler
    : IRequestHandler<RejectFriendRequestCommand>
{
    private readonly IFriendRequestRepository _friendRequestRepository;

    public RejectFriendRequestCommandHandler(
        IFriendRequestRepository friendRequestRepository)
    {
        _friendRequestRepository = friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
    }

    public async Task Handle(
        RejectFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var friend = await _friendRequestRepository
            .GetAsync(request.UserId, request.FriendUserId)
            ?? throw new InvalidOperationException("User is not a friend");

        friend.RejectFriendRequest();
        _friendRequestRepository.Update(friend);

        await _friendRequestRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
