using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Aggregates.FriendAggregate;

namespace UserService.Infrastructure.EntityConfigs;
internal class FriendEntityTypeConfiguration
    : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable("Friends");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.FriendUserId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
    }
}
