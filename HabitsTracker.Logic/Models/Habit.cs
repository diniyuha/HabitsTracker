using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Logic.Models
{
    public class Habit
    {
        //public Guid Id { get; set; }

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

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; }

        public List<HabitReminder> Reminders { get; set; } = new();

        public List<int> DayNumbers { get; set; } = new();
        
        public List<HabitTracking> TrackRecords { get; set; } = new();
    }
}