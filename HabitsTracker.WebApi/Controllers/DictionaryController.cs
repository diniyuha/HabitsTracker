using System;
using HabitsTracker.Data.Enums;
using HabitsTracker.Logic.Models;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        
        /// <summary>
        /// Получение справочника привычек
        /// </summary>
        /// <returns></returns>
        [HttpGet("habits")]
        public IActionResult GetHabitsDictionary()
        {
            var habitsDictionaryList = _dictionaryService.GetHabitsDictionary();
            return Ok(habitsDictionaryList);
        }

        /// <summary>
        /// Получение привычки из справочника по id
        /// </summary>
        /// <param name="id">id привычки</param>
        /// <returns></returns>
        [HttpGet("habits/{id}")]
        public IActionResult GetHabitsDictionaryById(Guid id)
        {
            var habitsDictionaryItem = _dictionaryService.GetHabitsDictionaryById(id);
            return Ok(habitsDictionaryItem);
        }

        /// <summary>
        /// Создание привычки в справочнике привычек (доступно пользователям с ролью Admin)
        /// </summary>
        /// <param name="habit">Данные привычки</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("habits/")]
        public IActionResult CreateHabit([FromBody] HabitsDictionary habit)
        {
            var habitUnit = _dictionaryService.CreateHabitsDictionary(habit);
            return Ok(habitUnit);
        }

        /// <summary>
        /// Удаление из справочника привычки(доступно пользователям с ролью Admin)
        /// </summary>
        /// <param name="id">id привычки</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteHabit(Guid id)
        {
            var habitsItem = _dictionaryService.GetHabitsDictionaryById(id);
            if (habitsItem == null)
            {
                return NotFound();
            }

            _dictionaryService.DeleteHabitsDictionary(id);
            return Ok();
        }

        /// <summary>
        /// Получение списка единиц измерений
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("units/{id}")]
        public IActionResult GetUnitById(Guid id)
        {
            var unit = _dictionaryService.GetUnitById(id);
            return Ok(unit);
        }
    }
}