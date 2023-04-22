using System;

namespace HabitsTracker.Logic.Models
{
    public class HabitFilter
    {
        public string? Name { get; set; }
        
        //public DateTime? CompletionDate{ get; set; }
        
        public Guid? UnitId { get; set; }
    }
}