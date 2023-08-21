using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Infrastructure.EntityConfigs;
internal class FollowerEntityTypeConfiguration
    : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder.ToTable("Followers");
        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.FolloweeUserId).IsRequired();

        builder.Property(x => x.FollowerUserId).IsRequired();
    }
}
