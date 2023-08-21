using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Infrastructure.EntityConfigs;
internal class UserEntityTypeConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.Name).IsRequired();
        
        builder.Property(u => u.Email).IsRequired();

        builder.HasMany(u => u.Friends)
            .WithOne()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Followers)
            .WithOne()
            .HasForeignKey(u => u.FollowerUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
