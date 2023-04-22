using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IDictionaryService
    {
        List<HabitsDictionary> GetHabitsDictionary();
        HabitsDictionary GetHabitsDictionary(Guid id);
        object GetHabitsDictionaryById(Guid id);
        object GetUnitById(Guid id);
    }
    
}