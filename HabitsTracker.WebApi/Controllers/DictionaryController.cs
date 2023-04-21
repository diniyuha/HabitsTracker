using System.Collections;
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

        [HttpGet()]
        public IActionResult GetHabitsDictionary()
        {
            var habitsDictionaryList = _dictionaryService.GetHabitsDictionary();
            return Ok(habitsDictionaryList);
        }
        //TODO сделать на основании DictionaryService
    }
}