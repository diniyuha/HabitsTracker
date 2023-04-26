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
                .Include(x => x.Frequencies)
                .Include(x => x.Reminders)
                .Include(x => x.TrackRecords)
                .Where(h => h.UserId == userId)
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
            var habit = _dbContext.Habits
                .Include(x => x.Frequencies)
                .Include(x => x.Reminders)
                .Include(x => x.TrackRecords)
                .FirstOrDefault(h => h.Id == id);
            if (habit == null)
            {
                throw new ArgumentException("Not found");
            }

            return _mapper.Map<Habit>(habit);
        }

        public Guid CreateHabit(ChangeHabitRequest habit, Guid userId)
        {
            var habitEntity = _mapper.Map<HabitEntity>(habit);
            habitEntity.Id = Guid.NewGuid();
            habitEntity.UserId = userId;
            habitEntity.Frequencies = habit.DayNumbers.Select(x => new FrequencyEntity
            {
                Id = Guid.NewGuid(),
                DayNumber = x
            }).ToList();

            habitEntity.Reminders = habit.Reminders.Select(x => new HabitReminderEntity
            {
                Id = Guid.NewGuid(),
                TimeReminder = x
            }).ToList();

            _dbContext.Habits.Add(habitEntity);
            _dbContext.SaveChanges();

            return habitEntity.Id;
        }

        public void UpdateHabit(Guid id, ChangeHabitRequest habit)
        {
            // Удаление связанных коллекций
            var frequencies = _dbContext.Frequencies.Where(x => x.HabitId == id).ToList();
            _dbContext.Frequencies.RemoveRange(frequencies);
            var reminders = _dbContext.HabitReminders.Where(x => x.HabitId == id).ToList();
            _dbContext.HabitReminders.RemoveRange(reminders);

            // Обновление полей привычки
            var habitEntity = _dbContext.Habits
                .Include(x => x.Frequencies)
                .Include(x => x.Reminders)
                .FirstOrDefault(x => x.Id == id);
            if (habitEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _mapper.Map(habit, habitEntity);
            _dbContext.Habits.Update(habitEntity);

            // Добавление связанных коллекций
            _dbContext.Frequencies.AddRange(habit.DayNumbers.Select(x => new FrequencyEntity
            {
                Id = Guid.NewGuid(),
                HabitId = id,
                DayNumber = x
            }));

            _dbContext.HabitReminders.AddRange(habit.Reminders.Select(x => new HabitReminderEntity
            {
                Id = Guid.NewGuid(),
                HabitId = id,
                TimeReminder = x
            }));

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
            var habit = _dbContext.Habits.Find(habitTrackingEntity.HabitId);
            if (habit == null)
            {
                throw new ArgumentException("Not found");
            }

            habitTrackingEntity.Habit = habit;
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
            var currentDayOfWeek = GetDayOfWeek(today.DayOfWeek);
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

        private int GetDayOfWeek(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Sunday)
            {
                return 7;
            }

            return (int) dayOfWeek;
        }


        public int CountCompletedGoal(HabitTrackingRequest request)
        {
            return _dbContext.HabitTracking
                .Where(x => x.HabitId == request.HabitId
                            && x.TrackingDate >= request.DateFrom.Date
                            && x.TrackingDate < request.DateTo.Date.AddDays(1))
                .Sum(x => x.GoalDone);
        }

        public Habit GetHabitWithTrackingRecords(HabitTrackingRequest request)
        {
            var habit = _dbContext.Habits.FirstOrDefault(x => x.Id == request.HabitId);
            if (habit == null)
            {
                throw new ArgumentException("Not found");
            }

            habit.TrackRecords = _dbContext.HabitTracking
                .Where(x => x.HabitId == request.HabitId
                            && x.TrackingDate >= request.DateFrom.Date
                            && x.TrackingDate < request.DateTo.Date.AddDays(1))
                .AsNoTracking()
                .ToList();
            return _mapper.Map<Habit>(habit);
        }
    }
}