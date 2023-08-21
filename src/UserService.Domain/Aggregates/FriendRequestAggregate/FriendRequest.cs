using UserService.Domain.Events;
using UserService.Domain.Exceptions;
using UserService.Domain.SeedWork;
using UserService.Domain.Services;

namespace UserService.Domain.Aggregates.FriendRequestAggregate;
public class FriendRequest : Entity, IAggregateRoot
{
    public Guid UserId { get; private set; }

    public Guid FriendUserId { get; private set; }

    public FriendshipStatus Status { get; private set; }

    private FriendRequest(Guid userId, Guid friendUserId, FriendshipStatus status)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FriendUserId = friendUserId;
        Status = status;
    }

    public static async Task<FriendRequest> SendFriendRequestAsync(
        Guid userId,
        Guid friendUserId,
        IFriendshipService friendshipService,
        IFriendRequestRepository friendRequestRepository)
    {
        if (userId == friendUserId)
        {
            throw new DomainException("User can not send friend request to himself!");
        }

        if (await friendshipService.AreTheyFriendsAsync(userId, friendUserId))
        {
            throw new DomainException("These users are already friends");
        }

        var friendRequest = await friendRequestRepository
            .GetPendingRequestAsync(userId, friendUserId);

        if (friendRequest is null)
        {
            friendRequest = new(userId, friendUserId, FriendshipStatus.Pending);

            friendRequest.AddDomainEvent(new FriendRequestSentDomainEvent(friendRequest));
            return friendRequest;
        }

        throw new FriendshipExistDomainException("User already has pending request");
    }

    public void AcceptFriendRequest()
    {
        if (Status != FriendshipStatus.Pending)
        {
            throw new DomainException("Not a pending friend request");
        }

        Status = FriendshipStatus.Accepted;

        AddDomainEvent(new FriendRequestAcceptedDomainEvent(this));

    }

    public void RejectFriendRequest()
    {
        if (Status != FriendshipStatus.Pending)
        {
            throw new DomainException("Not an active friend request");
        }

        Status = FriendshipStatus.Rejected;
    }
}
