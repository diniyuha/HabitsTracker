using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class HabitsDictionary
    {
        public Guid Id { get; set; }

        [Required] public string? Name { get; set; }

        public string? Description { get; set; }

        [Required] public int Goal { get; set; }

        [Required] public int GoalPeriod { get; set; }

        public DateTime? DateForm { get; set; }

        public DateTime? DateTo { get; set; }

        public string? Color { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}