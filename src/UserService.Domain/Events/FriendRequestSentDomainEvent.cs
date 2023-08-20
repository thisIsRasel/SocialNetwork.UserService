using MediatR;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Domain.Events;
public class FriendRequestSentDomainEvent : INotification
{
    public FriendRequest Friend { get; private set; }

    public FriendRequestSentDomainEvent(FriendRequest friend)
        => Friend = friend;
}
