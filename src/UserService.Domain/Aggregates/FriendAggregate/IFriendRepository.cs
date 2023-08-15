namespace UserService.Domain.Aggregates.FriendAggregate;
public interface IFriendRepository
{
    Task<Friend?> GetAsync(
        Guid userId, 
        Guid friendUserId);

    void Add(Friend friend);

    void Update(Friend friend);
}
