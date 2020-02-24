using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchEngines.Web.SearchEngines.Base;
using SearchEngines.Web.SearchEngines.Bing;
using SearchEngines.Web.SearchEngines.Google;
using SearchEngines.Web.SearchEngines.Yandex;

namespace SearchEngines.Web.Util
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Add instance of search search engine service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSearchEngineServices(this IServiceCollection services, IConfiguration configuration)
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

            services.AddSingleton(new SearchEngineServices()
            {
                SearchEngines = new List<ISearchEngine>()
                {
                    new GoogleSearchEngine(googleSearchOption),
                    new YandexSearchEngine(yandexSearchOption),
                    new BingSearchEngine(bingSearchOption)
                }
            });
        }
    }
}