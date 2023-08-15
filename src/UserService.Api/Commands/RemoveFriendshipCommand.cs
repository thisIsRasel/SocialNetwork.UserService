using MediatR;

namespace UserService.Api.Commands;

public class RemoveFriendshipCommand : IRequest
{
    public Guid UserId { get; private set; }

    public Guid FriendUserId { get; private set; }

    public RemoveFriendshipCommand(string userId, string friendUserId)
    {
        UserId = Guid.Parse(userId);
        FriendUserId = Guid.Parse (friendUserId);
    }
}
