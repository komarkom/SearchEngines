using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SearchEngines.Db.Entities;
using SearchEngines.Web.SearchEngines.Base;

namespace SearchEngines.Web.SearchEngines
{
    public class BingSearchEngine : ISearchEngine
    {
        private readonly BingSearchOption _bingSearchOption;

        public BingSearchEngine(BingSearchOption bingSearchOption)
        {
            _bingSearchOption = bingSearchOption;
        }

        public SearchResponse Search(string searchText, CancellationTokenSource cts)
        {
            HttpWebResponse response;
            try
            {
                var url = string.Format(
                    _bingSearchOption.BaseUrl,
                    searchText);

                response = SendRequest(url);
            }
            catch (Exception e)
            {
                return new SearchResponse() {HasError = true, Error = e.ToString()};
            }

            var result = ParseSearchResponse(response);

            if (result.HasError == false)
            {
                cts?.Cancel();
            }

            result.SearchSystemId = SearchSystem.DefaultRecord.FirstOrDefault(x => x.SystemName.Equals("bing"))?.Id;
            return result;
        }

        /// <summary>
        /// Send search request
        /// </summary>
        /// <param name="url">Formatted searching url</param>
        /// <returns>Web response</returns>
        private HttpWebResponse SendRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers["Ocp-Apim-Subscription-Key"] = _bingSearchOption.Key;

            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            return response;
        }

        /// <summary>
        /// Parsing web response with xml format
        /// </summary>
        /// <param name="response">Web response to parsing</param>
        /// <returns>Search response</returns>
        private SearchResponse ParseSearchResponse(HttpWebResponse response)
        {
            var result = new SearchResponse();
            BingResponse bingResponse;
            string responseBody;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }

            bingResponse = JsonConvert.DeserializeObject<BingResponse>(responseBody);

            result.Data = responseBody;
            if (bingResponse?.WebPages?.Value == null)
            {
                result.HasError = true;
                result.Error = "Not found anything";
                return result;
            }

            foreach (var item in bingResponse.WebPages.Value)
            {
                result.SearchResults.Add(new SearchResult()
                {
                    PreviewData = item.Snippet,
                    HeaderLinkText = item.Name,
                    Url = item.Url
                });
            }

            return result;
        }
    }

    public class BingResponse
    {
        public WebPages WebPages { get; set; }
    }

    public class Value
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Snippet { get; set; }
    }

    public class WebPages
    {
        public List<Value> Value { get; set; }
    }
}