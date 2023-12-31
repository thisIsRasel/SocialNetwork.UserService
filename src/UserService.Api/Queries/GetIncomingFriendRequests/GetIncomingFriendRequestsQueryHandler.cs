﻿using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;

namespace UserService.Api.Queries.GetIncomingFriendRequests;

public class GetIncomingFriendRequestsQueryHandler
    : IRequestHandler<GetIncomingFriendRequestsQuery, GetIncomingFriendRequestsQueryResponse>
{
    private readonly string _connectionString = string.Empty;

    public GetIncomingFriendRequestsQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<GetIncomingFriendRequestsQueryResponse> Handle(
        GetIncomingFriendRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var limit = 10;
        var offset = (request.Page - 1) * limit;

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var friendRequests = await connection.QueryAsync<IncomingFriendRequestDto>(
            @"SELECT u.Name, fr.UserId FROM FriendRequests fr 
                JOIN users u ON fr.UserId = u.Id 
                WHERE fr.FriendUserId = @UserId AND fr.Status = 1
                ORDER BY fr.Id
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY", new { request.UserId, limit, offset });

        return new GetIncomingFriendRequestsQueryResponse
        {
            FriendRequests = friendRequests.ToList(),
        };
    }
}
