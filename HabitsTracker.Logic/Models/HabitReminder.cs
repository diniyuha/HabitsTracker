using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class HabitReminder
    {
        public Guid Id { get; set; }

        [Required] public Guid HabitId { get; set; }

        public DateTime? TimeReminder { get; set; }
    }
}