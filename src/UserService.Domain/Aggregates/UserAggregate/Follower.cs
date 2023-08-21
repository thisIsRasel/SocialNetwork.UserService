using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.UserAggregate;
public class Follower : Entity
{
    public Guid FolloweeUserId { get; private set; }

    public Guid FollowerUserId { get; private set; }

    public Follower(Guid followeeUserId, Guid followerUserId) 
    {
        Id = Guid.NewGuid();
        FolloweeUserId = followeeUserId;
        FollowerUserId = followerUserId;
    }
}
