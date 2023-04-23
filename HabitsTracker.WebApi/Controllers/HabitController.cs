using HabitsTracker.Logic.Models;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.WebApi.Controllers
{
 
    [Authorize]
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

        [HttpGet("habits")]
        public IActionResult GetHabits(Guid userId, HabitFilter filter = null)
        {
            var habitsList = _habitService.GetHabits(userId, filter);
            return Ok(habitsList);
        }
       
        [HttpGet("habits/{id}")]
        public IActionResult GetHabitById(Guid id)
        {
            var habitsItem = _habitService.GetHabitById(id);
            return Ok(habitsItem);
        }
        
        [HttpGet("units/{habit}")]
        public IActionResult CreateHabit(Habit habit)
        {
            var habitUnit = _habitService.CreateHabit(habit);
            return Ok(habitUnit);
        }
        
        [HttpGet]
        public IActionResult DeleteHabit(Guid id) 
        {
            var userId = User.Identity?.Name;
            if (userId != null)
            {
                var habit = _habitService.GetHabitById(id);
                if (habit == null)
                {
                    return NotFound();
                }
                _habitService.DeleteHabit(habit.Id);
                return Ok();
            }
            return NotFound();
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateHabit(Guid id, [FromForm] Habit habit)
        {
            if (id != habit.Id)
            {
                return BadRequest();
            }

            _habitService.UpdateHabit(id, habit);
            return Ok();
        }
    }
}