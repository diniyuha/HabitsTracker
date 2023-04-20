using System;

namespace HabitsTracker.Data.Entities
{
    public class HabitReminderEntity
    {
        public Guid Id { get; set; }

        public Guid HabitId { get; set; }
        public HabitEntity Habit { get; set; }

        public DateTime TimeReminder { get; set; }
    }
}