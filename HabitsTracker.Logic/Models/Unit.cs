using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class Unit
    {
        public Guid Id { get; set; }

        [Required] public string Name { get; set; }
    }
}