using MediatR;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Api.Commands;

public sealed class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await User.CreateAsync(
            name: request.Name,
            email: request.Email,
            userRepository: _userRepository);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
