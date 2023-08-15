using MediatR;

namespace UserService.Api.Queries.GetFollowers;

public class GetFollowersQuery : IRequest<FollowersQueryResponse>
{
    public GetFollowersQuery(string userId, string query, int page)
    {
        UserId = Guid.Parse(userId);
        Query = query;
        Page = page;
    }

    public Guid UserId { get; private set; }

    public string Query { get; private set; }

    public int Page { get; private set; }
}
