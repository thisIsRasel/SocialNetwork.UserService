using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using UserService.Domain.Aggregates.FriendAggregate;

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
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var limit = 10;
        var offset = (request.Page - 1) * limit;

        var result = await connection.QueryAsync<dynamic>(
            @"SELECT u.Name, f.FriendUserId, f.Status FROM friends f 
                JOIN users u ON f.FriendUserId = u.Id 
                WHERE f.UserId = @UserId
                ORDER BY f.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, limit, offset });

        var friendQueryResponse = new FriendsQueryResponse();

        foreach (var item in result)
        {
            Enum.TryParse<FriendshipStatus>(item.Status.ToString(), out FriendshipStatus status);

            friendQueryResponse.Friends.Add(new FriendDto
            {
                FriendUserId = item.FriendUserId.ToString(),
                FriendName = item.Name,
                FriendshipStatus = status.ToString(),
            });
        }

        return friendQueryResponse;
    }
}
