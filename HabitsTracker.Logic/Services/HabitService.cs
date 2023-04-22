using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public class HabitService: IHabitService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public HabitService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        //TODO
        public List<Habit> GetHabits(HabitFilter filter = null)
        {
            var habits = _dbContext.Habits.ToList();
            return _mapper.Map<List<Habit>>(habits);
        }

        //TODO
        public Habit GetHabitById(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Guid CreateHabit(Habit habit)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public void DeleteHabit(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public void UpdateHabit(Guid id, Habit habit)
        {
            throw new  NotImplementedException();
        }
    }
}