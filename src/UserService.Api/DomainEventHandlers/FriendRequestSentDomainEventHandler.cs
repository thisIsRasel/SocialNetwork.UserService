using MediatR;
using UserService.Domain.Aggregates.FolloweeAggregate;
using UserService.Domain.Events;

namespace UserService.Api.DomainEventHandlers;

public class FriendRequestSentDomainEventHandler
    : INotificationHandler<FriendRequestSentDomainEvent>
{
    private readonly IFolloweeRepository _followeeRepository;

    public FriendRequestSentDomainEventHandler(
        IFolloweeRepository followeeRepository)
    {
        _followeeRepository = followeeRepository ?? throw new ArgumentNullException(nameof(followeeRepository));
    }

    public async Task Handle(
        FriendRequestSentDomainEvent notification,
        CancellationToken cancellationToken)
    {
        await Followee.Follow(
            notification.Friend.UserId,
            notification.Friend.FriendUserId,
            _followeeRepository);
    }
}
