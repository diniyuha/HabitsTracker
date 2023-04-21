using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Data.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        
        public string? Icon { get; set; }
        
        public Role Role { get; set; }
        public Language Language { get; set; }
        
        public ColorTheme ColorTheme { get; set; }
        
        public List<HabitEntity> Habits { get; set; } = new ();
    }
}