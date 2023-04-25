using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Data.Entities;
using HabitsTracker.Data.Enums;
using HabitsTracker.Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitsTracker.Logic.Services
{
    public class HabitService : IHabitService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public HabitService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Habit> GetHabits(Guid userId, HabitFilter? filter = null)
        {
            var query = _dbContext.Habits
                .Where(h => h.UserId == userId)
                .Include(x => x.Frequencies)
                .Include(x => x.Reminders)
                .AsNoTracking()
                .AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(o => o.Name.Contains(filter.Name));
                }

                if (filter.UnitId.HasValue)
                {
                    query = query.Where(x => x.UnitId == filter.UnitId.Value);
                }
            }

            var habits = query.ToList();
            return _mapper.Map<List<Habit>>(habits);
        }

        public Habit GetHabitById(Guid id)
        {
            var habit = _dbContext.Habits.Find(id);
            if (habit == null)
            {
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<Habit>(habit);
        }

        public Guid CreateHabit(Habit habit)
        {
            var habitEntity = _mapper.Map<HabitEntity>(habit);
            habitEntity.Id = Guid.NewGuid();
            habitEntity.Frequencies = habit.DayNumbers.Select(x => new FrequencyEntity
            {
                Id = Guid.NewGuid(),
                DayNumber = x
            }).ToList();

            _dbContext.Habits.Add(habitEntity);
            _dbContext.SaveChanges();

            return habitEntity.Id;
        }

        public void UpdateHabit(Guid id, Habit habit)
        {
            var habitEntity = _dbContext.Habits.Find(id);
            if (habitEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _mapper.Map(habit, habitEntity);
            habitEntity.Frequencies = habit.DayNumbers.Select(x => new FrequencyEntity
            {
                Id = Guid.NewGuid(),
                DayNumber = x
            }).ToList();

            _dbContext.SaveChanges();
        }

        public void DeleteHabit(Guid id)
        {
            var habitEntity = _dbContext.Habits.Find(id);
            if (habitEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _dbContext.Habits.Remove(habitEntity);
            _dbContext.SaveChanges();
        }

        public List<HabitTracking> GetTrackingRecordsByHabitId(Guid habitId, Guid userId)
        {
            var trackRecords = _dbContext.HabitTracking
                .Include(x => x.Habit)
                .Where(x => x.HabitId == habitId && x.Habit.UserId == userId)
                .AsNoTracking()
                .ToList();
            if (trackRecords == null)
            {
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<List<HabitTracking>>(trackRecords);
        }

        public HabitTracking GetTrackingRecordById(Guid id, Guid userId)
        {
            var trackRecord = _dbContext.HabitTracking
                .Include(x => x.Habit)
                .FirstOrDefault(x => x.Id == id && x.Habit.UserId == userId);
            if (trackRecord == null)
            {
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<HabitTracking>(trackRecord);
        }

        public Guid CreateHabitTracking(HabitTracking habitTracking, Guid userId)
        {
            var habitTrackingEntity = _mapper.Map<HabitTrackingEntity>(habitTracking);
            habitTrackingEntity.Id = Guid.NewGuid();
            habitTrackingEntity.Habit.UserId = userId;
            _dbContext.HabitTracking.Add(habitTrackingEntity);
            _dbContext.SaveChanges();

            return habitTrackingEntity.Id;
        }

        public void UpdateHabitTracking(Guid id, HabitTracking habitTracking)
        {
            var habitTrackingEntity = _dbContext.HabitTracking.Find(id);
            if (habitTrackingEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _mapper.Map(habitTracking, habitTrackingEntity);
            _dbContext.SaveChanges();
        }

        public void DeleteHabitTracking(Guid id)
        {
            var habitTrackingEntity = _dbContext.HabitTracking.Find(id);
            if (habitTrackingEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _dbContext.HabitTracking.Remove(habitTrackingEntity);
            _dbContext.SaveChanges();
        }
        
        public List<Habit> GetTodayHabits()
        {
            var today = DateTime.Today;
            var currentDayOfWeek = (int) today.DayOfWeek;
            var currentDayOfMonth = today.Day;

            var items = _dbContext.Habits
                .Include(x => x.Frequencies)
                .Where(x => (x.GoalPeriod == GoalPeriod.Day
                            || (x.GoalPeriod == GoalPeriod.Week &&
                                x.Frequencies.Any(f => f.DayNumber == currentDayOfWeek))
                            || (x.GoalPeriod == GoalPeriod.Month &&
                                x.Frequencies.Any(f => f.DayNumber == currentDayOfMonth))
                            )
                            && (!x.DateFrom.HasValue || x.DateFrom.Value <= today)
                            && (!x.DateTo.HasValue || x.DateTo.Value >= today)
                )
                .ToList();
            return _mapper.Map<List<Habit>>(items);
        }

        public int CountCompletedDays(Guid habitId, DateTime StartDate, DateTime CompletionDate)
        {
            var total = 0;
            var items = _dbContext.HabitTracking
                .Where(x => x.HabitId == habitId
                            && x.TrackingDate >= StartDate
                            && x.TrackingDate <= CompletionDate)
                .ToList();
            foreach (var record in items)
            {
                total += record.GoalDone;
            }

            return total;
        }
    }
}