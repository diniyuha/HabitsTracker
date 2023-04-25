using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HabitsTracker.Data;
using HabitsTracker.Data.Entities;
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

        public HabitsDictionary GetHabitsDictionaryById(Guid id)
        {
            var habit = _dbContext.HabitsDictionary.FirstOrDefault(x => x.Id == id);
            if (habit == null)
            {
                throw new ArgumentException("Not found");
            }
            return _mapper.Map<HabitsDictionary>(habit);
        }
        
        public Guid CreateHabitsDictionary(HabitsDictionary habit)
        {
            var habitEntity = _mapper.Map<HabitsDictionaryEntity>(habit);
            habitEntity.Id = Guid.NewGuid();
           
            var result = _dbContext.HabitsDictionary.Add(habitEntity);
            _dbContext.SaveChanges();

            return habitEntity.Id;
        }
        
        public void DeleteHabitsDictionary(Guid id)
        {
            var habitEntity = _dbContext.HabitsDictionary.Find(id);
            if (habitEntity == null)
            {
                throw new ArgumentException("Not found");
            }

            _dbContext.HabitsDictionary.Remove(habitEntity);
            _dbContext.SaveChanges();
        }
        
        public Unit GetUnitById(Guid id)
        {
            var unitEntity = _dbContext.Units.Find(id);
            if (unitEntity == null)
            {
                throw new ArgumentException("Not found");
            }
            return _mapper.Map<Unit>(unitEntity);
        }

    }
}