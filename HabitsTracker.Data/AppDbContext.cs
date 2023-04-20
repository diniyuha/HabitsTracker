using HabitsTracker.Data.Entities;
using HabitsTracker.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace HabitsTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<HabitsDictionaryEntity> HabitsDictionary { get; set; }

        public DbSet<HabitReminderEntity> HabitReminders { get; set; }

        public DbSet<HabitEntity> Habits { get; set; }

        public DbSet<FrequencyEntity> Frequencies { get; set; }

        public DbSet<UnitEntity> Units { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserEntity>()
                .Property(x => x.Language)
                .HasDefaultValue(Language.Ru);

            modelBuilder.Entity<UserEntity>()
                .Property(x => x.ColorTheme)
                .HasDefaultValue(ColorTheme.Light);
            
            modelBuilder.Entity<UserEntity>()
                .Property(x => x.Email)
                .IsRequired();
            
            modelBuilder.Entity<UserEntity>()
                .Property(x => x.Password)
                .IsRequired();

            modelBuilder.Entity<HabitsDictionaryEntity>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<HabitsDictionaryEntity>()
                .Property(x => x.GoalPeriod)
                .HasDefaultValue(GoalPeriod.Day);
            
            modelBuilder.Entity<HabitsDictionaryEntity>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<HabitEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<HabitEntity>()
                .Property(x => x.GoalPeriod)
                .HasDefaultValue(GoalPeriod.Day);
            
            modelBuilder.Entity<HabitEntity>()
                .Property(x => x.Name)
                .IsRequired();
            
            
            modelBuilder.Entity<HabitReminderEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<FrequencyEntity>().HasKey(x => x.Id);
         
            modelBuilder.Entity<FrequencyEntity>()
                .Property(x => x.HabitId)
                .IsRequired();
            
            modelBuilder.Entity<UnitEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<UnitEntity>()
                .Property(x => x.Name)
                .IsRequired();
            
            modelBuilder.Entity<HabitEntity>()
                .HasMany(x => x.Frequencies)
                .WithOne(x => x.Habit)
                .HasForeignKey(x => x.HabitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HabitEntity>()
                .HasMany(x => x.Reminders)
                .WithOne(x => x.Habit)
                .HasForeignKey(x => x.HabitId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.Habits)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}