using System;
using System.Threading;
using System.Threading.Tasks;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Base;

namespace SearchEngines.Web.SearchEngines
{
    public class GoogleSearchEngine:ISearchEngine
    {
        public SearchResponse Search(string searchText, CancellationTokenSource cts)
        {
            Thread.Sleep(15000);
            Console.WriteLine(searchText + "   google");
            return new SearchResponse() { SearchSystemId = 1 };

        }
    }
}