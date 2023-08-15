using MediatR;
using UserService.Domain.SeedWork;

namespace UserService.Infrastructure;
internal static class MediatorExtensions
{
    public static async void DispatchDomainEventsAsync(
        this IMediator mediator, UserDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents ?? new List<INotification>())
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
