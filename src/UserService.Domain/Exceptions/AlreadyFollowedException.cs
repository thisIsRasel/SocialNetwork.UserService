namespace UserService.Domain.Exceptions;
internal class AlreadyFollowedException : Exception
{
    public AlreadyFollowedException() { }

    public AlreadyFollowedException(string message) : base(message) { }
}
