using System.Collections;
using HabitsTracker.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabitsTracker.WebApi.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictionaryService;
        
        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        
        //TODO сделать на основании DictionaryService
    }
}