using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace UserService.Api.Queries.GetFollowings;

public class GetFollowingsQueryHandler
    : IRequestHandler<GetFollowingsQuery, GetFollowingsQueryResponse>
{
    public readonly string _connectionString = string.Empty;

    public GetFollowingsQueryHandler(
        IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<GetFollowingsQueryResponse> Handle(
        GetFollowingsQuery request, 
        CancellationToken cancellationToken)
    {
        var limit = 10;
        var offset = (request.Page - 1) * limit;

        var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            @"SELECT u.Name, f.FolloweeUserId FROM Followers f
                JOIN Users u ON u.Id = f.FolloweeUserId
                WHERE f.FollowerUserId = @UserId
                ORDER BY f.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, offset, limit }
            );

        GetFollowingsQueryResponse response = new();

        foreach(var item in result)
        {
            response.Followings.Add(new FollowingDto
            {
                FolloweeUserId = item.FolloweeUserId,
                FolloweeName = item.Name
            });
        }

        return response;
    }
}
