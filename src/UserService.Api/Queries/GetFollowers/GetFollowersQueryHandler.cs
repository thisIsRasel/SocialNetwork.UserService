using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace UserService.Api.Queries.GetFollowers;

public class GetFollowersQueryHandler
    : IRequestHandler<GetFollowersQuery, GetFollowersQueryResponse>
{
    private readonly string _connectionString = string.Empty;

    public GetFollowersQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<GetFollowersQueryResponse> Handle(
        GetFollowersQuery request,
        CancellationToken cancellationToken)
    {
        var limit = 10;
        var offset = (request.Page - 1) * limit;

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var followers = await connection.QueryAsync<FollowerDto>(
            @"SELECT u.Name FollowerName, f.FollowerUserId FROM Followers f 
                JOIN users u ON f.FollowerUserId = u.Id 
                WHERE f.FolloweeUserId = @UserId
                ORDER BY f.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, limit, offset });

        return new GetFollowersQueryResponse
        {
            Followers = followers.ToList()
        };
    }
}
