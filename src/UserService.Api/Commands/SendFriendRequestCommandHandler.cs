using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;
using UserService.Domain.Services;

namespace UserService.Api.Commands;

public sealed class SendFriendRequestCommandHandler
    : IRequestHandler<SendFriendRequestCommand>
{
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly IFriendshipService _friendshipService;

    public SendFriendRequestCommandHandler(
        IFriendRequestRepository friendRequestRepository,
        IFriendshipService friendshipService)
    {
        _friendRequestRepository = friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
        _friendshipService = friendshipService ?? throw new ArgumentNullException(nameof(friendshipService));
    }

    public async Task Handle(
        SendFriendRequestCommand request,
        CancellationToken cancellationToken)
    {
        var friendRequest = await FriendRequest.SendFriendRequestAsync(
            userId: request.UserId,
            friendUserId: request.FriendUserId,
            _friendshipService,
            _friendRequestRepository);

        _friendRequestRepository.Add(friendRequest);

        await _friendRequestRepository.UnitOfWork
            .SaveEntitiesAsync(cancellationToken);
    }
}
