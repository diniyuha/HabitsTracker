using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Data.Entities;
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

        public List<Habit> GetHabits(Guid userId, HabitFilter filter = null)
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

                if (filter.UnitId != Guid.Empty)
                {
                    query = query.Where(x => x.UnitId == filter.UnitId);
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
            var newHabit = new HabitEntity()
            {
                Frequencies = habit.DayNumbers.Select(x => new FrequencyEntity { DayNumber = x }).ToList(),
                Reminders = new List<HabitReminderEntity>((IEnumerable<HabitReminderEntity>)habit),
            };

            _dbContext.Habits.Add(newHabit);
            _dbContext.SaveChangesAsync();

            return Guid.NewGuid();
        }

        public void DeleteHabit(Guid id)
        {
            var habitEntity = _dbContext.Habits.Find(id);
            if (habitEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _dbContext.Habits.Remove(habitEntity);
            _dbContext.SaveChangesAsync();
        }

        public void UpdateHabit(Guid id, Habit habit)
        {
            var habitEntity = _dbContext.Habits.Find(id);
            if (habitEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _mapper.Map(habit, habitEntity);
            _dbContext.SaveChangesAsync();
        }
    }
}