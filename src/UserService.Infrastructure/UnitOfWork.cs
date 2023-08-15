using MediatR;
using UserService.Domain.SeedWork;

namespace UserService.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly UserDbContext _dbContext;

    public UnitOfWork(
        IMediator mediator,
        UserDbContext dbContext)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _mediator.DispatchDomainEventsAsync(_dbContext);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
