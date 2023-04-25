using System;
using System.Collections.Generic;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IHabitService
    {
        List<Habit> GetHabits(Guid userId, HabitFilter filter = null);
        Habit GetHabitById(Guid id);
        Guid CreateHabit(Habit habit);
        void DeleteHabit(Guid id);
        void UpdateHabit(Guid id,  Habit habit);
        List<HabitTracking> GetTrackingRecordsByHabitId(Guid habitId, Guid userId);
        Guid CreateHabitTracking(HabitTracking habitTracking, Guid userId);
        void UpdateHabitTracking(Guid id, HabitTracking habitTracking);
        void DeleteHabitTracking(Guid id);
        HabitTracking GetTrackingRecordById(Guid id, Guid userId);
        List<Habit> GetTodayHabits();
        int CountCompletedDays(Guid habitId, DateTime StartDate, DateTime CompletionDate);
    }
}