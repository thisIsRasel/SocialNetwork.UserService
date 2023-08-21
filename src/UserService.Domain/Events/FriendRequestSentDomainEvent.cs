using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Domain.Events;
public class FriendRequestSentDomainEvent : INotification
{
    public FriendRequest FriendRequest { get; private set; }

    public FriendRequestSentDomainEvent(FriendRequest friendRequest)
        => FriendRequest = friendRequest;
}
