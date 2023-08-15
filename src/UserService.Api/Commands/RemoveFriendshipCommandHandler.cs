using MediatR;
using UserService.Domain.Aggregates.FriendAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Api.Commands;

public sealed class RemoveFriendshipCommandHandler
    : IRequestHandler<RemoveFriendshipCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFriendRepository _friendRepository;

    public RemoveFriendshipCommandHandler(
        IUnitOfWork unitOfWork,
        IFriendRepository friendRepository)
    {
        _unitOfWork = unitOfWork;
        _friendRepository = friendRepository;
    }

    public async Task Handle(
        RemoveFriendshipCommand request,
        CancellationToken cancellationToken)
    {
        var friend = await _friendRepository
            .GetAsync(request.UserId, request.FriendUserId)
            ?? throw new InvalidOperationException("User is not a friend");

        friend.RemoveFriendship();

        _friendRepository.Update(friend);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
