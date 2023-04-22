using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.Data;
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

        //TODO
        public Guid CreateHabit(Habit habit)
        {
            throw new NotImplementedException();
        }

        //TODO
        public void DeleteHabit(Guid id)
        {
            throw new NotImplementedException();
        }

        //TODO
        public void UpdateHabit(Guid id, Habit habit)
        {
            throw new NotImplementedException();
        }

        public List<Habit> GetHabits(HabitFilter filter = null)
        {
            throw new NotImplementedException();
        }
    }
}