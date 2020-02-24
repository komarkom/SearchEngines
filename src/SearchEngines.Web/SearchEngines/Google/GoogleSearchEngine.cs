using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SearchEngines.Db.Entities;
using SearchEngines.Web.SearchEngines.Base;
using SearchEngines.Web.SearchEngines.Google.Model;

namespace SearchEngines.Web.SearchEngines.Google
{
    /// <summary>
    /// Implementation google search engine
    /// </summary>
    ///<inheritdoc cref="ISearchEngine"/>
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly GoogleSearchOption _googleSearchOption;

        /// <summary>
        /// New instance of GoogleSearchEngine
        /// </summary>
        /// <param name="googleSearchOption">Setting</param>
        public GoogleSearchEngine(GoogleSearchOption googleSearchOption)
        {
            _googleSearchOption = googleSearchOption;
        }

        public async Task<SearchResponse> Search(string searchText, CancellationTokenSource cts)
        {
            HttpWebResponse response;
            try
            {
                var url = string.Format(
                    _googleSearchOption.BaseUrl,
                    _googleSearchOption.Cx,
                    _googleSearchOption.Key,
                    searchText);

                response = await DoSearch(url);
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

            result.SearchSystemId = SearchSystem.DefaultRecord.FirstOrDefault(x => x.SystemName.Equals("google"))?.Id;
            return result;
        }

        /// <summary>
        /// Send search request and get response
        /// </summary>
        /// <param name="url">Formatted searching url</param>
        /// <returns>Web response</returns>
        private async Task<HttpWebResponse> DoSearch(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();

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
            string responseBody;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            
            var googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(responseBody);

            if (googleResponse == null)
            {
                var googleErrorResponse = JsonConvert.DeserializeObject<GoogleErrorResponse>(responseBody);
                result.HasError = true;
                result.Error = googleErrorResponse?.Error?.Message ?? "Internal error";
                return result;
            }

            result.Data = responseBody;
            if (googleResponse.Items == null)
            {
                result.HasError = true;
                result.Error = "Not found anything";
                return result;
            }

            foreach (var item in googleResponse.Items)
            {
                result.SearchResults.Add(new SearchResult()
                {
                    PreviewData = item.Snippet,
                    HeaderLinkText = item.Title,
                    Url = item.Link
                });
            }

            return result;
        }
    }
}