using System;

namespace HabitsTracker.Logic.Models
{
    public class HabitTrackingRequest
    {
        public Guid HabitId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}