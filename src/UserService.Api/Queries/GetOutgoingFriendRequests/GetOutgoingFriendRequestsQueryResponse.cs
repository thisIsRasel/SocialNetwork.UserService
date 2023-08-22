namespace UserService.Api.Queries.GetOutgoingFriendRequests;

public class GetOutgoingFriendRequestsQueryResponse
{
    public List<OutgoingFriendRequestDto> FriendRequests { get; set; } = new();
}

public class OutgoingFriendRequestDto
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;
}
