using System;
using HabitsTracker.Data.Enums;

namespace HabitsTracker.Logic.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        
        public string? Icon { get; set; }
      
        public Role Role { get; set; }
        
        public Language Language { get; set; }
        
        public ColorTheme ColorTheme { get; set; }
    }
}