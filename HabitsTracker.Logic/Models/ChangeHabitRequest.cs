using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Logic.Models
{
    public class ChangeHabitRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// Цель в числовом эквиваленте
        /// </summary>
        public int Goal { get; set; }

        /// <summary>
        /// Период повтора (daily, weekly, monthly)
        /// </summary>
        public GoalPeriod GoalPeriod { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public Guid UnitId { get; set; }
      
        public List<DateTime> Reminders { get; set; } = new();

        public List<int> DayNumbers { get; set; } = new();
        
    }
}