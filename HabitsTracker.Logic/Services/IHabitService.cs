using System;
using System.Collections.Generic;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IHabitService
    {
        List<Habit> GetHabits(Guid userId, HabitFilter filter = null);
        Habit GetHabitById(Guid id);
        void DeleteHabit(Guid id);
        void UpdateHabit(Guid id, ChangeHabitRequest habit);
        List<HabitTracking> GetTrackingRecordsByHabitId(Guid habitId, Guid userId);
        Guid CreateHabitTracking(HabitTracking habitTracking, Guid userId);
        void UpdateHabitTracking(Guid id, HabitTracking habitTracking);
        void DeleteHabitTracking(Guid id);
        HabitTracking GetTrackingRecordById(Guid id, Guid userId);
        List<Habit> GetTodayHabits();
        Habit GetHabitWithTrackingRecords(HabitTrackingRequest request);
        int CountCompletedGoal(HabitTrackingRequest request);
        Guid CreateHabit(ChangeHabitRequest habit, Guid userId);
    }
}