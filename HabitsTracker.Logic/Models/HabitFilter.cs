using System;

namespace HabitsTracker.Logic.Models
{
    public class HabitFilter
    {
        internal readonly Guid UnitId;

        public string? Name { get; set; }
        
        public DateTime? CompletionDate{ get; set; }
    }
}