using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskFocus.Data.Entities;


namespace TaskFocus.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        
        public DbSet<UserEntity> Users { get; set; }
        
        public DbSet<TaskManagerUserSettingsEntity> TaskManagerUserSettings { get; set; }
        
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}