using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Domain.Services;
public class FriendshipService : IFriendshipService
{
    private readonly IUserRepository _userRepository;

    public FriendshipService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<bool> AreTheyFriendsAsync(Guid userId, Guid friendUserId)
    {
        var user = await _userRepository.GetAsync(userId) 
            ?? throw new InvalidOperationException($"Not a valid user: {userId}");

        if (user.Friends.Any(x => x.FriendUserId == friendUserId))
        {
            return true;
        }

        var friendUser = await _userRepository.GetAsync(friendUserId)
            ?? throw new InvalidOperationException($"Not a valid user: {friendUserId}");

        if (friendUser.Friends.Any(x => x.FriendUserId == userId)) {
            return true;
        }

        return false;
    }
}
