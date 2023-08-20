using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.UserAggregate;
public class Friend : Entity
{
    public Guid UserId { get; private set; }

    public Guid FriendUserId { get; private set; }

    public Friend(Guid userId, Guid friendUserId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        FriendUserId = friendUserId;
    }
}
