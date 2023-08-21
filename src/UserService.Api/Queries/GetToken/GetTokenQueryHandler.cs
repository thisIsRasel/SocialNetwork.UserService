using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace UserService.Api.Queries.GetToken;

public class GetTokenQueryHandler
    : IRequestHandler<GetTokenQuery, GetTokenQueryResponse>
{
    private readonly string _connectionString = string.Empty;
    private readonly IConfiguration _configuration;

    public GetTokenQueryHandler(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionString = configuration.GetConnectionString("Default")!;
    }

    public async Task<GetTokenQueryResponse> Handle(
        GetTokenQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var result = await connection.QueryAsync<dynamic>(
            @"SELECT u.Id, u.Name, u.Email FROM users u WHERE u.Email = @Email", new { request.Email });

        var user = result.FirstOrDefault()
            ?? throw new InvalidOperationException("Not a valid user");

        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = new Dictionary<string, object>
            {
                { "userId", user.Id },
                { "name", user.Name },
                { "email", user.Email }
            },
            Issuer = "me",
            Audience = "you",
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return new GetTokenQueryResponse
        {
            AccessToken = tokenHandler.WriteToken(securityToken)
        };
    }
}
