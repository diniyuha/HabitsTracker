using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IDictionaryService
    {
        Task<List<Habit>> GetHabitsDictionary();
        Task<Habit> GetHabitsDictionary(Guid id);
        
        Task<List<int>> GetFrequenciesByHabitId(Guid habitId);
        
        Task<List<string>> GetRemindersByHabitId(Guid habitId);
    
    }
}