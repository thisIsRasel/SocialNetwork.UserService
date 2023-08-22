using MediatR;

namespace UserService.Api.Queries.GetFollowings;

public class GetFollowingsQuery : IRequest<GetFollowingsQueryResponse>
{
    public Guid UserId { get; private set; }

    public int Page { get; private set; }

    public GetFollowingsQuery(string userId, int page)
    {
        UserId = Guid.Parse(userId);
        Page = page;
    }
}
