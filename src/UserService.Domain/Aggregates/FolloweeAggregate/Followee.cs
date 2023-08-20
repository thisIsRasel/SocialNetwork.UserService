using UserService.Domain.Exceptions;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.FolloweeAggregate;
public class Followee : Entity, IAggregateRoot
{
    public Guid UserId { get; private set; }

    public Guid FolloweeUserId { get; private set; }

    public FollowStatus FollowStatus { get; private set; }

    private Followee(Guid userId, Guid followeeUserId, FollowStatus followStatus)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FolloweeUserId = followeeUserId;
        FollowStatus = followStatus;
    }

    public static async Task<Followee> Follow(
        Guid userId,
        Guid followeeUserId,
        IFolloweeRepository followeeRepository)
    {
        if (userId == followeeUserId)
        {
            throw new DomainException("User can not follow himself");
        }

        var followee = await followeeRepository.GetAsync(userId, followeeUserId);

        if (followee is null)
        {
            followee = new(userId, followeeUserId, FollowStatus.Followed);
            followeeRepository.Add(followee);
            return followee;
        }

        if (followee.FollowStatus == FollowStatus.Unfollowed)
        {
            followee.FollowStatus = FollowStatus.Followed;
            followeeRepository.Update(followee);
            return followee;
        }

        throw new AlreadyFollowedException("User is already followed");
    }

    public void Unfollow()
    {
        if (FollowStatus == FollowStatus.Unfollowed)
        {
            throw new DomainException("User is already unfollowed");
        }

        FollowStatus = FollowStatus.Unfollowed;
    }
}
