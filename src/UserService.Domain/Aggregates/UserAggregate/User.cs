using UserService.Domain.Exceptions;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.UserAggregate;
public partial class User : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    private User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new DomainException(nameof(name));
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new DomainException(nameof(email));
    }

    public static async Task<User> CreateAsync(
        string name,
        string email,
        IUserRepository userRepository)
    {
        var user = await userRepository.GetByEmailAsync(email);

        if (user is not null)
        {
            throw new DomainException($"User exist with email: {email}");
        }

        user = new(name, email);
        return user;
    }
}
