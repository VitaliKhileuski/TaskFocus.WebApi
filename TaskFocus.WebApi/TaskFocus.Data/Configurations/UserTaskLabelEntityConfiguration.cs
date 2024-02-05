using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Configurations;

public class UserTaskLabelEntityConfiguration : IEntityTypeConfiguration<UserTaskLabelEntity>
{
    public void Configure(EntityTypeBuilder<UserTaskLabelEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
    }
}