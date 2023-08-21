namespace UserService.Domain.Exceptions;
internal class AlreadyFollowedDomainException : Exception
{
    public AlreadyFollowedDomainException() { }

    public AlreadyFollowedDomainException(string message) : base(message) { }
}
