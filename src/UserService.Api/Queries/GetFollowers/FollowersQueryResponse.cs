namespace UserService.Api.Queries.GetFollowers;

public class FollowersQueryResponse
{
    public List<FollowerDto> Followers { get; set; } = new();
}

public class FollowerDto
{
    public string FollowerUserId { get; set; } = default!;

    public string FollowerName { get; set; } = default!;
}