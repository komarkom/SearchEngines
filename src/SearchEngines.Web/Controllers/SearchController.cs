using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchEngines.Db.Context;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Base;
using SearchEngines.Web.Util;

namespace SearchEngines.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchEnginesDbContext _context;
        private readonly SearchEngineServices _searchEngineServices;

        public SearchController(SearchEnginesDbContext context, SearchEngineServices searchEngineServices)
        {
            _context = context;
            _searchEngineServices = searchEngineServices;
        }

        // GET: api/Search
        [HttpGet("")]
        public async Task<ActionResult> Search(string searchText)
        {
            var tasks = new List<Task>();
            foreach (var searchEngine in _searchEngineServices.SearchEngines)
            {
                tasks.Add(new Task<SearchResponse>(()=> searchEngine.Search(searchText)));
            }

            foreach (var task in tasks)
            {
                task.Start();
            }

            var res = Task.WaitAny(tasks.ToArray());
            return new JsonResult(1);
        }

        // GET: api/Search/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SearchResult>> GetSearchResult(long id)
        {
            var searchResult = await _context.SearchResults.FindAsync(id);

            if (searchResult == null)
            {
                return NotFound();
            }

            return searchResult;
        }
    }
}
