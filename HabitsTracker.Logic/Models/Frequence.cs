using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class Frequence
    {
        public Guid Id { get; set; }
        
        public Guid HabitId { get; set; }
        public Habit Habit { get; set; }
        
        /// <summary>
        /// Номер дня недели (для GoalPeriod неделя)
        /// Номер дня месяца (для GoalPeriod месяц)
        /// </summary>
        public int DayNumber { get; set; }
    }
}