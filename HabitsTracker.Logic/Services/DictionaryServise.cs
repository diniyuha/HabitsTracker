using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        //TODO
        public DictionaryService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        //TODO
        public Task<List<Habit>> GetHabitsDictionary()
        {
            // var habitsDictionary =  _dbContext.HabitsDictionary.ToListAsync();
            //
            // return  _mapper.Map<List<Habit>>(habitsDictionary);
            throw new  NotImplementedException();
        }

        //TODO
        public Task<Habit> GetHabitsDictionary(Guid id)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task<List<int>> GetFrequenciesByHabitId(Guid habitId)
        {
            throw new  NotImplementedException();
        }

        //TODO
        public Task<List<string>> GetRemindersByHabitId(Guid habitId)
        {
            throw new  NotImplementedException();
        }
    }
}