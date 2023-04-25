using System;

namespace HabitsTracker.Logic.Models
{
    public class HabitFilter
    {
        public Guid? UnitId { get; set; }

        public string? Name { get; set; }
        
        //public DateTime? CompletionDate{ get; set; }
    }
}