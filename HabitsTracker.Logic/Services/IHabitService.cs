using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IHabitService
    {
        List<Habit> GetHabits(HabitFilter filter = null);
        Habit GetHabitById(Guid id);
        Guid CreateHabit(Habit habit);
        void DeleteHabit(Guid id);
        void UpdateHabit(Guid id,  Habit habit);
    }
}