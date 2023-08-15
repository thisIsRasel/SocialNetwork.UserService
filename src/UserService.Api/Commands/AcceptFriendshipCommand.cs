﻿using MediatR;

namespace UserService.Api.Commands;

public class AcceptFriendshipCommand: IRequest
{
    public Guid UserId { get; private set; }

    public Guid FriendUserId { get; private set; }

    public AcceptFriendshipCommand(string userId, string friendUserId)
    {
        UserId = Guid.Parse(userId);
        FriendUserId = Guid.Parse(friendUserId);
    }
}
