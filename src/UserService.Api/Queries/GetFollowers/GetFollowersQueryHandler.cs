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

    public Task<FollowersQueryResponse> Handle(
        GetFollowersQuery request, 
        CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var followersQueryResponse = new FollowersQueryResponse();

        return Task.FromResult(followersQueryResponse);
    }
}
