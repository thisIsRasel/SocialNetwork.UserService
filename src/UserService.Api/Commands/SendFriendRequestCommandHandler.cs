using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Api.Commands;

public sealed class SendFriendRequestCommandHandler
    : IRequestHandler<SendFriendRequestCommand>
{
    private readonly IFriendRequestRepository _friendRequestRepository;

    public SendFriendRequestCommandHandler(
        IFriendRequestRepository friendRequestRepository)
    {
        _friendRequestRepository = friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
    }

    public async Task Handle(
        SendFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var friendRequest = await FriendRequest.SendFriendRequestAsync(
            userId: request.UserId,
            friendUserId: request.FriendUserId,
            _friendRequestRepository);

        _friendRequestRepository.Add(friendRequest);

        await _friendRequestRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
