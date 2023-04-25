using System;
using System.Linq;
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

        /// <summary>
        /// Получения списка привычек с использованием фильтров(необязательно для заполнения)
        /// </summary>
        /// <param name="filter">Фильтры: единица измерения, наименование</param>
        /// <returns></returns>
        [HttpPost("list")]
        public IActionResult GetHabits([FromBody] HabitFilter? filter = null)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitsList = _habitService.GetHabits(user.Id, filter);
            return Ok(habitsList);
        }

        /// <summary>
        /// Получение привычки по id
        /// </summary>
        /// <param name="id">id привычки</param>
        /// <returns></returns>
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

        /// <summary>
        /// Создание новой привычки
        /// </summary>
        /// <param name="habit">Данные привычки</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateHabit([FromBody] ChangeHabitRequest habit)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitUnit = _habitService.CreateHabit(habit, user.Id) ;
            return Ok(habitUnit);
        }

        /// <summary>
        /// Удаление привычки по id
        /// </summary>
        /// <param name="id">id привычки</param>
        /// <returns></returns>
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

            _habitService.DeleteHabit(id);
            return Ok();
        }

        /// <summary>
        /// Редактирование привычки 
        /// </summary>
        /// <param name="id">id привычки</param>
        /// <param name="habit">измененные данные</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateHabit(Guid id, [FromBody] ChangeHabitRequest habit)
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

            _habitService.UpdateHabit(id, habit);
            return Ok();
        }
    }
}