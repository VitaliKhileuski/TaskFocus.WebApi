using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Configurations;

public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Label)
            .WithOne()
            .HasForeignKey<TaskEntity>(x => x.LabelId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(x => x.Priority)
            .WithOne()
            .HasForeignKey<TaskEntity>(x => x.PriorityId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}