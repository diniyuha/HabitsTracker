using System;
using System.ComponentModel.DataAnnotations;

namespace HabitsTracker.Logic.Models
{
    public class User
    {
        public Guid Id { get; set; }
        
        [Required] public string Email { get; set; }
        
        [Required] public string Password { get; set; }
        
        public string? Name { get; set; }
        
        public string? Surname { get; set; }
        
        public string? Icon { get; set; }
        
        public int Language { get; set; }
        
        public string? ColorTheme { get; set; }
    }
}