using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IHabitService
    {
        Task<List<Habit>> GetHabits(HabitFilter filter = null);
        Task<Habit> GetHabitById(int id);
        Task<int> CreateHabit(Habit habit);
        Task DeleteHabit(int id);
        Task UpdateHabit(int id,  Habit habit);
    }
}