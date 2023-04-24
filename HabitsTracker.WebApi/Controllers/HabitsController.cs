using System;
using HabitsTracker.Logic.Models;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsController : Controller
    {
        private readonly IHabitService _habitService;
        private readonly IUserService _userService;

        public HabitsController(IHabitService habitService, IUserService userService)
        {
            _habitService = habitService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetHabits(HabitFilter filter = null)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitsList = _habitService.GetHabits(user.Id, filter);
            return Ok(habitsList);
        }

        [HttpGet("{id}")]
        public IActionResult GetHabitById(Guid id)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitsItem = _habitService.GetHabitById(id);
            if (habitsItem.UserId != user.Id)
            {
                return Forbid();
            }

            return Ok(habitsItem);
        }

        [HttpPost]
        public IActionResult CreateHabit([FromForm] Habit habit)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            habit.UserId = user.Id;
            var habitUnit = _habitService.CreateHabit(habit);
            return Ok(habitUnit);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHabit(Guid id)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitsItem = _habitService.GetHabitById(id);
            if (habitsItem == null)
            {
                return NotFound();
            }

            if (habitsItem.UserId != user.Id)
            {
                return Forbid();
            }

            _habitService.DeleteHabit(habitsItem.Id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHabit(Guid id, [FromForm] Habit habit)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitsItem = _habitService.GetHabitById(id);
            if (habitsItem == null)
            {
                return NotFound();
            }

            if (habitsItem.UserId != user.Id)
            {
                return Forbid();
            }

            habit.UserId = user.Id;
            _habitService.UpdateHabit(id, habit);
            return Ok();
        }
    }
}