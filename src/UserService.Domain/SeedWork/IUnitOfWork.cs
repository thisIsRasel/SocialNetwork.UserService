namespace UserService.Domain.SeedWork;
public interface IUnitOfWork
{
    Task SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
