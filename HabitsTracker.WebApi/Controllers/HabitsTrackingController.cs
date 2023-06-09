﻿using System;
using HabitsTracker.Logic.Models;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HabitsTrackingController : Controller
    {
        private readonly IHabitService _habitService;
        private readonly IUserService _userService;

        public HabitsTrackingController(IHabitService habitService, IUserService userService)
        {
            _habitService = habitService;
            _userService = userService;
        }

        /// <summary>
        /// Получение зафиксированных результатов по выбранной привычке
        /// </summary>
        /// <param name="habitId">id привычки</param>
        /// <returns></returns>
        [HttpGet("{habitId}")]
        public IActionResult GetTrackingRecordsByHabitId(Guid habitId)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitRecords = _habitService.GetTrackingRecordsByHabitId(habitId, user.Id);

            return Ok(habitRecords);
        }

        /// <summary>
        /// Добавление записи выполнения привычки 
        /// </summary>
        /// <param name="habitTracking">Данные трекинга: дата, количество</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateHabitTracking([FromBody] HabitTracking habitTracking)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitRecord = _habitService.CreateHabitTracking(habitTracking, user.Id);
            return Ok(habitRecord);
        }

        /// <summary>
        ///  Редактирование данных выполненной привычки
        /// </summary>
        /// <param name="id">id записи выполнения</param>
        /// <param name="habitTracking">Измененные данные трекинга: дата, количество</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateHabitTracking(Guid id, [FromBody] HabitTracking habitTracking)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitRecord = _habitService.GetTrackingRecordById(id, user.Id);
            if (habitRecord == null)
            {
                return NotFound();
            }

            habitTracking.HabitId = user.Id;
            _habitService.UpdateHabitTracking(id, habitTracking);
            return Ok();
        }

        /// <summary>
        /// Удаление записи о выполнении привычки
        /// </summary>
        /// <param name="id">id записи выполненной привычки</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteHabitTracking(Guid id)
        {
            var user = _userService.GetUserByEmail(User.Identity?.Name);
            var habitTracking = _habitService.GetTrackingRecordById(id, user.Id);
            if (habitTracking == null)
            {
                return NotFound();
            }

            _habitService.DeleteHabitTracking(id);
            return Ok();
        }

        /// <summary>
        /// Получение привычек,которые нужно выполнить на сегодня
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTodayHabits()
        {
            var habits = _habitService.GetTodayHabits();
            return Ok(habits);
        }


        /// <summary>
        /// Получение общей суммы выполненных целей для выбранной привычки за указанный период
        /// </summary>
        /// <param name="request">Состоит из: id привычки и периода</param>
        /// <returns></returns>
        [HttpPost("totalGoal")]
        public IActionResult GetCompletedGoalByHabitId(HabitTrackingRequest request)
        {
            var totalGoalDone = _habitService.CountCompletedGoal(request);
            return Ok(totalGoalDone);
        }

        /// <summary>
        /// Получение списка с зафиксированными результатами за указанный период для выбранной привычки
        /// </summary>
        /// <param name="request">Состоит из: id привычки и периода</param>
        /// <returns></returns>
        [HttpPost("records")]
        public IActionResult GetHabitWithTrackingRecords(HabitTrackingRequest request)
        {
            var records = _habitService.GetHabitWithTrackingRecords(request);
            return Ok(records);
        }
    }
}