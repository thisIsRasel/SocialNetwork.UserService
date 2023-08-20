using MediatR;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Domain.Events;

namespace UserService.Api.DomainEventHandlers;

public class FriendRequestAcceptedDomainEventHandler
    : INotificationHandler<FriendRequestAcceptedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    public FriendRequestAcceptedDomainEventHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task Handle(
        FriendRequestAcceptedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(notification.FriendRequest.UserId) 
            ?? throw new InvalidOperationException("Not a valid user");

        var friendUser = await _userRepository.GetAsync(notification.FriendRequest.FriendUserId)
            ?? throw new InvalidOperationException("Not a valid user");

        user.AddFriend(friendUser.Id);
        _userRepository.Update(user);

        friendUser.AddFriend(user.Id);
        _userRepository.Update(friendUser);
    }
}
