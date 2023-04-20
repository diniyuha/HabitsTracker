using System;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Data.Entities
{
    public class HabitsDictionaryEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int Goal { get; set; }

        public GoalPeriod GoalPeriod { get; set; }

        public DateTime? DateForm { get; set; }

        public DateTime? DateTo { get; set; }

        public Guid UnitId { get; set; }
        public UnitEntity Unit { get; set; }
    }
}