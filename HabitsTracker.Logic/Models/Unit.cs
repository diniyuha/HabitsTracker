using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class Unit
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}