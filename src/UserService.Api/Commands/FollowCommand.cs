using MediatR;

namespace UserService.Api.Commands;

public class FollowCommand : IRequest
{
    public Guid UserId { get; private set; }

    public Guid FolloweeUserId { get; private set; }

    public FollowCommand(string userId, string followeeUserId)
    {
        UserId = Guid.Parse(userId);
        FolloweeUserId = Guid.Parse(followeeUserId);
    }
}
