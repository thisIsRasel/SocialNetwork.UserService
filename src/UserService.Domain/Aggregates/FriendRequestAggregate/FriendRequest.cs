using UserService.Domain.Events;
using UserService.Domain.Exceptions;
using UserService.Domain.SeedWork;

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
        IFriendRequestRepository friendRequestRepository)
    {
        if (userId == friendUserId)
        {
            throw new DomainException("User can not send friend request to himself!");
        }

        var friendRequest = await friendRequestRepository.GetAsync(userId, friendUserId);

        if (friendRequest is null)
        {
            friendRequest = new(userId, friendUserId, FriendshipStatus.Pending);
            friendRequestRepository.Add(friendRequest);

            friendRequest.AddDomainEvent(new FriendRequestSentDomainEvent(friendRequest));
            return friendRequest;
        }

        if (friendRequest.Status == FriendshipStatus.Rejected)
        {
            friendRequest.Status = FriendshipStatus.Pending;
            friendRequestRepository.Update(friendRequest);

            friendRequest.AddDomainEvent(new FriendRequestSentDomainEvent(friendRequest));
            return friendRequest;
        }

        throw new FriendshipExistException("User already has pending or accepted request");
    }

    public void AcceptFriendRequest()
    {
        if (Status != FriendshipStatus.Pending)
        {
            throw new DomainException("Do not have any pending friend request");
        }

        Status = FriendshipStatus.Accepted;

        AddDomainEvent(new FriendRequestAcceptedDomainEvent(this));
    }

    public void RejectFriendRequest()
    {
        if (Status == FriendshipStatus.Rejected)
        {
            throw new DomainException("Not an active friend");
        }

        Status = FriendshipStatus.Rejected;
    }
}
