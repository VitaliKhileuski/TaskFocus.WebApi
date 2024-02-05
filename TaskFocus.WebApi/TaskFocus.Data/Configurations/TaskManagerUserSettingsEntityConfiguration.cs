using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Configurations;

public class TaskManagerUserSettingsEntityConfiguration : IEntityTypeConfiguration<TaskManagerUserSettingsEntity>
{
    public void Configure(EntityTypeBuilder<TaskManagerUserSettingsEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany(x => x.Labels)
            .WithOne()
            .HasForeignKey(x => x.TaskManagerUserSettingsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Priorities)
            .WithOne()
            .HasForeignKey(x => x.TaskManagerUserSettingsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}