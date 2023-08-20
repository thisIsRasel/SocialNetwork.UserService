using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Aggregates.FolloweeAggregate;

namespace UserService.Infrastructure.EntityConfigs;
internal class FolloweeEntityTypeConfiguration
    : IEntityTypeConfiguration<Followee>
{
    public void Configure(EntityTypeBuilder<Followee> builder)
    {
        builder.ToTable("Followees");
        builder.Ignore(b => b.DomainEvents);
        
        builder.HasKey(x => x.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.FolloweeUserId).IsRequired();
        
        builder.Property(x => x.FollowStatus).IsRequired();
    }
}
