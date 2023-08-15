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
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired();
        builder.Property(u => u.Email).IsRequired();

        //builder.HasMany(u => u.Friends)
        //    .WithOne()
        //    .HasForeignKey(u => u.UserId)
        //    .OnDelete(DeleteBehavior.Cascade);

        //builder.HasMany(u => u.Followees)
        //    .WithOne()
        //    .HasForeignKey(u => u.UserId)
        //    .OnDelete(DeleteBehavior.Cascade);
    }
}
