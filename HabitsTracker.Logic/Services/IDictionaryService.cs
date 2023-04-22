﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HabitsTracker.Logic.Models;

namespace HabitsTracker.Logic.Services
{
    public interface IDictionaryService
    {
        List<HabitsDictionary> GetHabitsDictionary();
        HabitsDictionary GetHabitsDictionaryById(Guid id);
        Unit GetUnitById(Guid id);
    }
    
}