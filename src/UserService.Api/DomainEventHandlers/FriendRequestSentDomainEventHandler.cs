using MediatR;
using UserService.Domain.Events;

namespace UserService.Api.DomainEventHandlers;

public class FriendRequestSentDomainEventHandler
    : INotificationHandler<FriendRequestSentDomainEvent>
{
    public Task Handle(FriendRequestSentDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
