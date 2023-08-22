namespace UserService.Api.Queries.GetIncomingFriendRequests;

public class GetIncomingFriendRequestsQueryResponse
{
    public List<IncomingFriendRequestDto> FriendRequests { get; set; } = new();
}

public class IncomingFriendRequestDto
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = default!;
}