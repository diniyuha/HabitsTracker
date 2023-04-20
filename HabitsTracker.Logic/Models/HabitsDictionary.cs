using System;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Logic.Models
{
    public class HabitsDictionary
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int Goal { get; set; }

        public GoalPeriod GoalPeriod { get; set; }

        public DateTime? DateForm { get; set; }

        public DateTime? DateTo { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}