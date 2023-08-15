namespace UserService.Domain.Exceptions;
internal class FriendshipExistException
    : Exception
{
    public FriendshipExistException() { }

    public FriendshipExistException(string message): base(message) { }
}
