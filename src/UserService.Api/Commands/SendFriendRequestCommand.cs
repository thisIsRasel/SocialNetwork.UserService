using MediatR;

namespace UserService.Api.Commands;

public class SendFriendRequestCommand : IRequest
{
    public Guid UserId { get; private set; }

    public Guid FriendUserId { get; private set; }

    public SendFriendRequestCommand(string userId, string friendUserId)
    {
        UserId = Guid.Parse(userId);
        FriendUserId = Guid.Parse(friendUserId);
    }
}
