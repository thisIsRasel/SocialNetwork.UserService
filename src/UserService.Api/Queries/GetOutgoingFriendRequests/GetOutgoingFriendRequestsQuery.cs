using MediatR;

namespace UserService.Api.Queries.GetOutgoingFriendRequests;

public class GetOutgoingFriendRequestsQuery
    : IRequest<GetOutgoingFriendRequestsQueryResponse>
{
    public Guid UserId { get; private set; }

    public int Page { get; private set; }

    public GetOutgoingFriendRequestsQuery(string userId, int page)
    {
        UserId = Guid.Parse(userId);
        Page = page;
    }
}
