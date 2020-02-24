using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngines.Db.Context;
using SearchEngines.Db.Entities;
using SearchEngines.Web.DTO;
using SearchEngines.Web.Models;
using SearchEngines.Web.Util;

namespace SearchEngines.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SearchEnginesDbContext _context;
        private readonly ISearchManager _searchManager;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, SearchEnginesDbContext context, ISearchManager searchManager, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _searchManager = searchManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return View();
            }

            var request = new SearchRequest() {SearchText = searchText};
            _context.SearchRequests.Add(request);
            _context.SaveChanges();

            var res = _searchManager.Search(searchText);
            if (!res.IsOk || res.Value == null)
            {
                _logger.LogInformation(res.ErrorMessage);
                return View("Index", new SearchResponseDto()
                {
                    HasError = true,
                    Error = res.ErrorMessage
                });
            }

            res.Value.SearchRequestId = request.Id;
            _context.SearchResponses.Add(res.Value);
            _context.SaveChanges();

            var resultDto = _mapper.Map<SearchResponseDto>(res.Value);
            var searchSystem = _context.SearchSystems.Find(res.Value?.SearchSystemId);
            resultDto.SearchSystem = searchSystem?.SystemName ?? resultDto.SearchSystem;

            return View("Index", resultDto);
        }

        public IActionResult OfflineSearch(string searchText, int? take = 10)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return View();
            }

            var count = _context.SearchResults.Count(x => x.HeaderLinkText.ToUpper().Contains(searchText.ToUpper()));
            if (count > 10)
            {
                ViewBag.InfoMessage = "There are more 10 found result, please specify your request";
            }
            var resultsFromDb =
                _context.SearchResults.Where(x => x.HeaderLinkText.ToUpper().Contains(searchText.ToUpper()))
                    .Take(take ?? 10);

            var results = _mapper.Map<ICollection<SearchResultDto>>(resultsFromDb);
            return View(results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
