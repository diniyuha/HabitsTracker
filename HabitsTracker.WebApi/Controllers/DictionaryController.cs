using System;
using HabitsTracker.Logic.Services;
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

        [HttpGet("habits")]
        public IActionResult GetHabitsDictionary()
        {
            var habitsDictionaryList = _dictionaryService.GetHabitsDictionary();
            return Ok(habitsDictionaryList);
        }

        [HttpGet("habits/{id}")]
        public IActionResult GetHabitsDictionaryById(Guid id)
        {
            var habitsDictionaryItem = _dictionaryService.GetHabitsDictionaryById(id);
            return Ok(habitsDictionaryItem);
        }
        
        [HttpGet("units/{id}")]
        public IActionResult GetUnitById(Guid id)
        {
            var unit = _dictionaryService.GetUnitById(id);
            return Ok(unit);
        }
    }
}