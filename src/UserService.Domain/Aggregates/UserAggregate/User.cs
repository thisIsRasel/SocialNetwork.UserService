using UserService.Domain.Exceptions;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Aggregates.UserAggregate;
public partial class User : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    public string Email { get; private set; }

    private readonly List<Friend> _friends;
    public IReadOnlyCollection<Friend> Friends => _friends;

    private User(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new DomainException(nameof(name));
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new DomainException(nameof(email));
        _friends = new List<Friend>();
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

    public void AddFriend(Guid friendUserId)
    {
        if (_friends.Any(f => f.FriendUserId == friendUserId))
        {
            throw new DomainException($"{friendUserId} is already a friend");
        }

        _friends.Add(new Friend(Id, friendUserId));
    }

    public void RemoveFriend(Guid friendUserId)
    {
        var friend = _friends.FirstOrDefault(f => f.FriendUserId == friendUserId) 
            ?? throw new DomainException($"{friendUserId} is not a friend");

        _friends.Remove(friend);
    }
}
