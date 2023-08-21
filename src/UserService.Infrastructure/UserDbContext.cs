using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.FriendRequestAggregate;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Domain.SeedWork;
using UserService.Infrastructure.EntityConfigs;

namespace UserService.Infrastructure;

public class UserDbContext : DbContext, IUnitOfWork
{
    public UserDbContext(
        DbContextOptions<UserDbContext> options, 
        IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Friend> Friends { get; set; }

    public DbSet<Follower> Followers { get; set; }

    public DbSet<FriendRequest> FriendRequests { get; set; }

    private readonly IMediator _mediator;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new FriendEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new FollowerEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new FriendRequestEntityTypeConfiguration());
    }

    public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        await base.SaveChangesAsync(cancellationToken);
    }
}
