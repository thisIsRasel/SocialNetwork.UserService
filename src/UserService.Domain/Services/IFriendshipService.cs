namespace UserService.Domain.Services;
public interface IFriendshipService
{
    Task<bool> AreTheyFriendsAsync(Guid userId, Guid friendUserId);
}
