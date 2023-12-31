﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Aggregates.UserAggregate;

namespace UserService.Infrastructure.EntityConfigs;
internal class FriendEntityTypeConfiguration
    : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable("Friends");
        builder.Ignore(b => b.DomainEvents);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.UserId).IsRequired();
        
        builder.Property(x => x.FriendUserId).IsRequired();
    }
}
