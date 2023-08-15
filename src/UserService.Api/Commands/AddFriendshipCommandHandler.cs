using MediatR;
using UserService.Domain.Aggregates.FriendAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Api.Commands;

public sealed class AddFriendshipCommandHandler
    : IRequestHandler<AddFriendshipCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFriendRepository _friendRepository;

    public AddFriendshipCommandHandler(
        IUnitOfWork unitOfWork,
        IFriendRepository friendRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _friendRepository = friendRepository ?? throw new ArgumentNullException(nameof(friendRepository));
    }

    public async Task Handle(
        AddFriendshipCommand request,
        CancellationToken cancellationToken)
    {
        var friend = await Friend.AddFriendshipAsync(
            userId: request.UserId,
            friendUserId: request.FriendUserId,
            _friendRepository);

        _friendRepository.Add(friend);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
