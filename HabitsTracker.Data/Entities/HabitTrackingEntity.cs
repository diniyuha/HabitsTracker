using System;

namespace HabitsTracker.Data.Entities
{
    public class HabitTrackingEntity
    {
        public Guid Id { get; set; }

        public HabitEntity Habit { get; set; } 
        public Guid HabitId { get; set; }

        public DateTime TrackingDate { get; set; }

        public int GoalDone { get; set; }
    }
}