using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Base;

namespace SearchEngines.Web.SearchEngines
{
    public class YandexSearchEngine: ISearchEngine
    {
        public  SearchResponse Search(string searchText, CancellationTokenSource cts)
        {
            // // GetAsync returns a Task<HttpResponseMessage>.   
            // HttpResponseMessage response = await client.GetAsync(url, ct);

            Thread.Sleep(1000);
            Console.WriteLine(searchText + "   yandex");
            cts.Cancel();
            return new SearchResponse()
            {
                SearchSystemId = 2, Data = searchText + "   yandex",
                SearchResults = new List<SearchResult>()
                {
                    new SearchResult() {PreviewData = "asdas sada", HeaderLinkText = "Yandex1", Url = "#"},
                    new SearchResult() {PreviewData = "werwe sada", HeaderLinkText = "Yandex2", Url = "#"},
                    new SearchResult() {PreviewData = "asdas dfewrweda", HeaderLinkText = "Yandex3", Url = "#"},
                    new SearchResult() {PreviewData = "nhgnfg sada", HeaderLinkText = "Yandex4", Url = "#"},
                },
            };
        }
    }
}