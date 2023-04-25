using System;

namespace HabitsTracker.Logic.Models
{
    public class HabitTracking
    {
        //public Guid Id { get; set; }

        public Guid HabitId { get; set; }

        public DateTime TrackingDate { get; set; }

        public int GoalDone { get; set; }
    }
}