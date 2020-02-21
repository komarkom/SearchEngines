using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngines.Db.Context;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Base;
using SearchEngines.Web.Models;
using SearchEngines.Web.Util;

namespace SearchEngines.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SearchEnginesDbContext _context;
        private readonly SearchEngineServices _searchEngineServices;


        public HomeController(ILogger<HomeController> logger, SearchEnginesDbContext context,  SearchEngineServices searchEngineServices)
        {
            _logger = logger;
            _context = context;
            _searchEngineServices = searchEngineServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpPost("Search")]
        public async Task<ActionResult> Search([FromBody] SearchRequestModel request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.SearchText))
            {
                return BadRequest("Value cannot be empty");
            }

            var tasks = new List<Task>();
            foreach (var searchEngine in _searchEngineServices.SearchEngines)
            {
                tasks.Add(new Task<SearchResponse>(() => searchEngine.Search(request.SearchText)));
            }

            foreach (var task in tasks)
            {
                task.Start();
            }

            var res = Task.WaitAny(tasks.ToArray());
            return new JsonResult(request.SearchText);
        }
    }
}
