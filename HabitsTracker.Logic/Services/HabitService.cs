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
        public Task<List<Habit>> GetHabits(HabitFilter filter = null)
        {
             throw new  NotImplementedException();
        }

        //TODO
        public Task<Habit> GetHabitById(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task<Guid> CreateHabit(Habit habit)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task DeleteHabit(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task UpdateHabit(Guid id, Habit habit)
        {
            throw new  NotImplementedException();
        }
    }
}