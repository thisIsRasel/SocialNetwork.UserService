using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Domain.Events;
public class FriendRequestAcceptedDomainEvent : INotification
{
    public FriendRequest FriendRequest { get; private set; }

    public FriendRequestAcceptedDomainEvent(FriendRequest friendRequest)
        => FriendRequest = friendRequest;
}
