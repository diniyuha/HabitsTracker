using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IDictionaryService
    {
        Task<List<Habit>> GetHabitsDictionary();
        Task<Habit> GetHabitsDictionary(int id);
        
        Task<List<int>> GetFrequencyByHabitId(int habitId);
        
        Task<List<string>> GetRemindersByHabitId(int habitId);
    
    }
}