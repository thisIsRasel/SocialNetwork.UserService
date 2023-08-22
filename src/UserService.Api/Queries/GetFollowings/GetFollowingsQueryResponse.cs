namespace UserService.Api.Queries.GetFollowings;

public class GetFollowingsQueryResponse
{
    public List<FollowingDto> Followings { get; set; } = new();
}

public class FollowingDto
{
    public Guid FolloweeUserId { get; set; }

    public string FolloweeName { get; set; } = default!;
}
