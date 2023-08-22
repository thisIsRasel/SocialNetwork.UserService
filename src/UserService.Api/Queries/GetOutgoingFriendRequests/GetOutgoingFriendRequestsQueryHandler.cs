using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace UserService.Api.Queries.GetOutgoingFriendRequests;

public class GetOutgoingFriendRequestsQueryHandler
    : IRequestHandler<GetOutgoingFriendRequestsQuery, GetOutgoingFriendRequestsQueryResponse>
{
    private readonly string _connectionString = string.Empty;

    public GetOutgoingFriendRequestsQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<GetOutgoingFriendRequestsQueryResponse> Handle(
        GetOutgoingFriendRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var limit = 10;
        var offset = (request.Page - 1) * limit;

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            @"SELECT u.Name, fr.UserId FROM FriendRequests fr 
                JOIN users u ON fr.FriendUserId = u.Id 
                WHERE fr.UserId = @UserId AND fr.Status = 1
                ORDER BY fr.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, limit, offset });

        GetOutgoingFriendRequestsQueryResponse response = new();

        foreach (var item in result)
        {
            response.FriendRequests.Add(new OutgoingFriendRequestDto
            {
                UserId = item.UserId,
                Name = item.Name,
            });
        }

        return response;
    }
}
