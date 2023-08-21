namespace UserService.Domain.Exceptions;
internal class FriendshipExistDomainException
    : Exception
{
    public FriendshipExistDomainException() { }

    public FriendshipExistDomainException(string message): base(message) { }
}
