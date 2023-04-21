using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public DictionaryService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<HabitsDictionary> GetHabitsDictionary()
        {
            var habitsDictionary = _dbContext.HabitsDictionary.ToList();
            return _mapper.Map<List<HabitsDictionary>>(habitsDictionary);
        }

        public HabitsDictionary GetHabitsDictionary(Guid id)
        {
            var habit = _dbContext.HabitsDictionary.Find(id);
            if (habit == null)
            {
                throw new ArgumentException("Not found");
            }
            return _mapper.Map<HabitsDictionary>(habit);
        }
    }
}