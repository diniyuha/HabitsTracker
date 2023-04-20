using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Data.Entities
{
    public class FrequencyEntity
    {
        public Guid Id { get; set; }
        
        public Guid HabitId { get; set; }
        public HabitEntity Habit { get; set; }
        
        /// <summary>
        /// Номер дня недели (для GoalPeriod неделя)
        /// Номер дня месяца (для GoalPeriod месяц)
        /// </summary>
        public int DayNumber { get; set; }
    }
}