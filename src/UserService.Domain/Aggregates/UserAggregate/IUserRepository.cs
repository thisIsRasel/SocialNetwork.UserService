namespace UserService.Domain.Aggregates.UserAggregate;
public interface IUserRepository
{
    Task<User?> GetAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task AddAsync(User user);
}
