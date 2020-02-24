using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SearchEngines.Web.SearchEngines.Base;
using SearchEngines.Web.SearchEngines.Bing;
using SearchEngines.Web.SearchEngines.Google;
using SearchEngines.Web.SearchEngines.Yandex;

namespace SearchEngines.Web.Util
{
    /// <summary>
    /// Search engines service
    /// </summary>
    public class SearchEngineServices: ISearchEngineServices
    {
        public SearchEngineServices(IConfiguration configuration)
        {
            var yandexSearchOption = configuration
                .GetSection("YandexSearchOption")
                .Get<YandexSearchOption>();

            var googleSearchOption = configuration
                .GetSection("GoogleSearchOption")
                .Get<GoogleSearchOption>();

            var bingSearchOption = configuration
                .GetSection("BingSearchOption")
                .Get<BingSearchOption>();

            SearchEngines = new List<ISearchEngine>()
            {
                new GoogleSearchEngine(googleSearchOption),
                new YandexSearchEngine(yandexSearchOption),
                new BingSearchEngine(bingSearchOption)
            };
        }

        /// <summary>
        /// Collection of implementation of search engine
        /// </summary>
        public ICollection<ISearchEngine> SearchEngines { get; set; }
    }
}