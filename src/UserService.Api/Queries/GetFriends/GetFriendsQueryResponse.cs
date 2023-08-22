namespace UserService.Api.Queries.GetFriends;

public class GetFriendsQueryResponse
{
    public List<FriendDto> Friends { get; set; } = new();
}

public class FriendDto
{
    public Guid FriendUserId { get; set; } = default!;

    public string FriendName { get; set; } = default!;
}
