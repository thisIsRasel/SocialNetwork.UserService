using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace UserService.Api.Queries.GetFollowers;

public class GetFollowersQueryHandler
    : IRequestHandler<GetFollowersQuery, FollowersQueryResponse>
{
    private readonly string _connectionString = string.Empty;

    public GetFollowersQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<FollowersQueryResponse> Handle(
        GetFollowersQuery request,
        CancellationToken cancellationToken)
    {
        var limit = 10;
        var offset = (request.Page - 1) * limit;

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            @"SELECT u.Name, f.UserId FROM followees f 
                JOIN users u ON f.UserId = u.Id 
                WHERE f.FollowStatus = 1 AND f.FolloweeUserId = @UserId
                ORDER BY f.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, limit, offset });

        var followersQueryResponse = new FollowersQueryResponse();

        foreach (var item in result)
        {
            followersQueryResponse.Followers.Add(new FollowerDto
            {
                FollowerUserId = item.UserId.ToString(),
                FollowerName = item.Name,
            });
        }

        return followersQueryResponse;
    }
}
