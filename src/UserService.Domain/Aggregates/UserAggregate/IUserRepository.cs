using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.UserAggregate;
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    void Add(User user);

    void Update(User user);
}
