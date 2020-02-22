using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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
        private readonly ISearchManager _searchManager;


        public HomeController(ILogger<HomeController> logger, SearchEnginesDbContext context, ISearchManager searchManager)
        {
            _logger = logger;
            _context = context;
            _searchManager = searchManager;
        }

        public IActionResult Index(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return View();
            }

            var res = _searchManager.Search(searchText);
            if (!res.IsOk)
            {
                _logger.LogInformation(res.ErrorMessage);
                return View("Index");
            }

            var responseModel = new SearchResponseModel()
            {
                Data = res.Value.Data,
                SearchResults = res.Value.SearchResults.Select(x => new SearchResultModel()
                {
                    HeaderLinkText = x.HeaderLinkText,
                    PreviewData = x.PreviewData,
                    Url = x.Url
                }).ToList()
            };

            return View("Index", responseModel);
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
    }
}
