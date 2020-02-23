using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchEngines.Web.SearchEngines;
using SearchEngines.Web.SearchEngines.Base;

namespace SearchEngines.Web.Util
{
    public static class ServiceProviderExtensions
    {
        public static void AddSearchEngineServices(this IServiceCollection services, IConfiguration configuration)
        {
            var yandexSearchOption = configuration
                .GetSection("YandexSearchOption")
                .Get<YandexSearchOption>();

            services.AddSingleton(new SearchEngineServices()
            {
                SearchEngines = new List<ISearchEngine>()
                {
                    // new GoogleSearchEngine(),
                    new YandexSearchEngine(yandexSearchOption)
                }
            });
        }
    }
}