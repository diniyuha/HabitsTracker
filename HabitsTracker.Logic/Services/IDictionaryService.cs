using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IDictionaryService
    {
        Task<List<Habit>> GetHabitsDictionary();
        Task<Habit> GetHabitsDictionary(int id);
        
        Task<Unit> GetUnitById(int id);
        Task<int> CreateUser(Unit unit);
        Task DeleteUnit(int id);
        Task UpdateUnit(int id, Unit unit);
        
        Task<Frequence> GetFrequenceById(int id);
        Task<int> CreateFrequence(Frequence frequence);
        Task DeleteFrequence(int id);
        Task UpdateFrequence(int id, Frequence frequence);
    }
}