using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class Habit
    {
        public Guid Id { get; set; }
        
        [Required] public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        /// <summary>
        /// Цель в числовом эквиваленте
        /// </summary>
        [Required] public int Goal { get; set; }
        
        /// <summary>
        /// Период повтора (daily, weekly, monthly)
        /// </summary>
        [Required] public int GoalPeriod { get; set; }
        
        public DateTime? DateForm { get; set; }
        
        public DateTime? DateTo { get; set; }
        
        public string? Color { get; set; }
        
        public Guid? UserId { get; set; }

        public Guid UnitId { get; set; }
        public Unit  Unit { get; set; } 
        
        public List<HabitReminder> Reminders { get; set; } = new List<HabitReminder>();
        
        public List<Frequence> Frequences { get; set; } = new List<Frequence>();
    }
}