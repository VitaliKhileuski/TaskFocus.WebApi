using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.TaskManagerUserSettings)
            .WithOne()
            .HasForeignKey<TaskManagerUserSettingsEntity>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}