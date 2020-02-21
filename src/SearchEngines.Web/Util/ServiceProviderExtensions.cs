using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SearchEngines.Web.Base;
using SearchEngines.Web.SearchEngines;

namespace SearchEngines.Web.Util
{
    public static class ServiceProviderExtensions
    {
        public static void AddSearchEngineServices(this IServiceCollection services)
        {
            services.AddSingleton(new SearchEngineServices()
            {
                SearchEngines = new List<ISearchEngine>()
                {
                    new GoogleSearchEngine(),
                    new YandexSearchEngine()
                }
            });
        }
    }
}