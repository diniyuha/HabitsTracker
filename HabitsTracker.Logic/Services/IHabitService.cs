using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IHabitService
    {
        Task<List<Habit>> GetHabits(HabitFilter filter = null);
        Task<Habit> GetHabitById(Guid id);
        Task<Guid> CreateHabit(Habit habit);
        Task DeleteHabit(Guid id);
        Task UpdateHabit(Guid id,  Habit habit);
    }
}