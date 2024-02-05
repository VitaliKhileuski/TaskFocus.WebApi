using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Configurations;

public class UserPriorityLevelEntityConfiguration : IEntityTypeConfiguration<UserPriorityLevelEntity>
{
    public void Configure(EntityTypeBuilder<UserPriorityLevelEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
    }
}