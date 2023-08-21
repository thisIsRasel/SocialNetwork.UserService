using MediatR;

namespace UserService.Api.Queries.GetToken;

public class GetTokenQuery : IRequest<GetTokenQueryResponse>
{
    public string Email { get; private set; }

    public GetTokenQuery(string email)
    {
        Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentNullException(nameof(email));
    }
}
