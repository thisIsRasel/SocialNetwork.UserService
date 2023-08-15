using MediatR;

namespace UserService.Api.Queries.GetFriends;

public class GetFriendsQuery : IRequest<FriendsQueryResponse>
{
    public GetFriendsQuery(string userId, string query, int page)
    {
        UserId = Guid.Parse(userId);
        Query = query;
        Page = page;
    }

    public Guid UserId { get; private set; }

    public string Query { get; private set; }

    public int Page { get; private set; }
}
