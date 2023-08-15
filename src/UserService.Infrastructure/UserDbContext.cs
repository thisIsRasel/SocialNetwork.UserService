using Microsoft.EntityFrameworkCore;
using UserService.Domain.Aggregates.FolloweeAggregate;
using UserService.Domain.Aggregates.FriendAggregate;
using UserService.Domain.Aggregates.UserAggregate;
using UserService.Infrastructure.EntityConfigs;

namespace UserService.Infrastructure;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Friend> Friends { get; set; }

    public DbSet<Followee> Followees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new FriendEntityTypeConfiguration());

        modelBuilder.ApplyConfiguration(new FolloweeEntityTypeConfiguration());
    }
}
