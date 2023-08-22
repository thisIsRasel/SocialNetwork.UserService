namespace UserService.Api.Queries.GetFollowers;

public class GetFollowersQueryResponse
{
    public List<FollowerDto> Followers { get; set; } = new();
}

public class FollowerDto
{
    public Guid FollowerUserId { get; set; } = default!;

    public string FollowerName { get; set; } = default!;
}