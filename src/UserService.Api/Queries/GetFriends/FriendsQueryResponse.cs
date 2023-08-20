namespace UserService.Api.Queries.GetFriends;

public class FriendsQueryResponse
{
    public List<FriendDto> Friends { get; set; } = new();
}

public class FriendDto
{
    public string FriendUserId { get; set; } = default!;

    public string FriendName { get; set; } = default!;
}
