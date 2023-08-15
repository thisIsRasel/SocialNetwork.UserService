namespace UserService.Domain.Aggregates.FolloweeAggregate;
public interface IFolloweeRepository
{
    Task<Followee?> GetAsync(
        Guid userId,
        Guid followeeUserId);

    void Add(Followee followee);

    void Update(Followee followee);
}
