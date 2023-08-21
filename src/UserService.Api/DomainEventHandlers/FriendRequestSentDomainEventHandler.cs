using MediatR;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Domain.Events;

namespace UserService.Api.DomainEventHandlers;

public class FriendRequestSentDomainEventHandler
    : INotificationHandler<FriendRequestSentDomainEvent>
{
    private readonly IUserRepository _userRepository;

    public FriendRequestSentDomainEventHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task Handle(
        FriendRequestSentDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(notification.FriendRequest.UserId)
            ?? throw new InvalidOperationException("Not a valid user");

        var followee = await _userRepository.GetAsync(notification.FriendRequest.FriendUserId)
            ?? throw new InvalidOperationException("Not a valid followee");

        user.Follow(followee.Id);
        _userRepository.Update(user);
    }
}
