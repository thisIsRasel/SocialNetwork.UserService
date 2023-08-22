using MediatR;

namespace UserService.Api.Queries.GetIncomingFriendRequests;

public class GetIncomingFriendRequestsQuery
    : IRequest<GetIncomingFriendRequestsQueryResponse>
{
    public Guid UserId { get; private set; }

    public int Page { get; private set; }

    public GetIncomingFriendRequestsQuery(string userId, int page)
    {
        UserId = Guid.Parse(userId);
        Page = page;
    }
}
