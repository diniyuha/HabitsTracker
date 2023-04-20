using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.WebApi.Controllers
{
    public class HabitController : Controller
    {
        private readonly IHabitService _habitService;
        private readonly IDictionaryService _dictionaryService;
        
        public HabitController(IHabitService habitService, IDictionaryService dictionaryService)
        {
            _habitService = habitService;
            _dictionaryService = dictionaryService;
        }
        
        //TODO сделать на основании HabitService
    }
}