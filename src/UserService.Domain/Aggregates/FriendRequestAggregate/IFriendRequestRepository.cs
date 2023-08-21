using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.FriendRequestAggregate;
public interface IFriendRequestRepository 
    : IRepository<FriendRequest>
{
    Task<FriendRequest?> GetPendingRequestAsync(
        Guid userId,
        Guid friendUserId);

    void Add(FriendRequest friend);

    void Update(FriendRequest friend);
}
