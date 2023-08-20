using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace UserService.Api.Queries.GetFriends;

public class GetFriendsQueryHandler
    : IRequestHandler<GetFriendsQuery, FriendsQueryResponse>
{
    private readonly string _connectionString = string.Empty;

    public GetFriendsQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<FriendsQueryResponse> Handle(
        GetFriendsQuery request,
        CancellationToken cancellationToken)
    {
        var limit = 10;
        var offset = (request.Page - 1) * limit;

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            @"SELECT u.Name, f.FriendUserId FROM friends f 
                JOIN users u ON f.FriendUserId = u.Id 
                WHERE f.UserId = @UserId
                ORDER BY f.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, limit, offset });

        var friendsQueryResponse = new FriendsQueryResponse();

        foreach (var item in result)
        {
            friendsQueryResponse.Friends.Add(new FriendDto
            {
                FriendUserId = item.FriendUserId.ToString(),
                FriendName = item.Name
            });
        }

        return friendsQueryResponse;
    }
}
