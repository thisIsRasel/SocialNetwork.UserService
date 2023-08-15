using UserService.Domain.Events;
using UserService.Domain.Exceptions;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.FriendAggregate;
public class Friend : Entity, IAggregateRoot
{
    public Guid UserId { get; private set; }

    public Guid FriendUserId { get; private set; }

    public FriendshipStatus Status { get; private set; }

    private Friend(Guid userId, Guid friendUserId, FriendshipStatus status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FriendUserId = friendUserId;
        Status = status;
    }

    public static async Task<Friend> AddFriendshipAsync(
        Guid userId,
        Guid friendUserId,
        IFriendRepository friendRepository)
    {
        if (userId == friendUserId)
        {
            throw new DomainException("User can not send friend request to himself!");
        }

        var friend = await friendRepository.GetAsync(userId, friendUserId);

        if (friend is null)
        {
            friend = new(userId, friendUserId, FriendshipStatus.Pending);

            friend.AddDomainEvent(new FriendRequestSentDomainEvent());
            return friend;
        }

        if (friend.Status == FriendshipStatus.Rejected)
        {
            friend.Status = FriendshipStatus.Pending;

            friend.AddDomainEvent(new FriendRequestSentDomainEvent());
            return friend;
        }

        throw new FriendshipExistException("User already has pending or accepted request");
    }

    public void AcceptFriendship()
    {
        if (Status != FriendshipStatus.Pending)
        {
            throw new DomainException("Do not have any pending friend request");
        }

        Status = FriendshipStatus.Accepted;
    }

    public void RemoveFriendship()
    {
        if (Status != FriendshipStatus.Rejected)
        {
            throw new DomainException("Not an active friend");
        }

        Status = FriendshipStatus.Rejected;
    }
}
