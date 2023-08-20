using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Aggregates.FriendRequestAggregate;

namespace UserService.Infrastructure.EntityConfigs;
internal class FriendRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.ToTable("FriendRequests");
        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.FriendUserId).IsRequired();
        
        builder.Property(x => x.Status).IsRequired();
    }
}
