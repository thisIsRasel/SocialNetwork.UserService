using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.FolloweeAggregate;
public interface IFolloweeRepository
    : IRepository<Followee>
{
    Task<Followee?> GetAsync(
        Guid userId,
        Guid followeeUserId);

    void Add(Followee followee);

    void Update(Followee followee);
}
