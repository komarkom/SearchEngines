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
    ///<inheritdoc cref="IWebRequestSearchEngine"/>
    ///<inheritdoc cref="IJsonResultWebSearchEngine"/>
    public class GoogleSearchEngine : ISearchEngine, IWebRequestSearchEngine, IJsonResultWebSearchEngine
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
                var url = GetFormattedSearchUrl(searchText);

                response = await DoSearch(url);
            }
            catch (Exception e)
            {
                return new SearchResponse() {HasError = true, Error = e.ToString()};
            }

            string responseBody;

            try
            {
                responseBody = ReadResponseString(response);
            }
            catch (Exception e)
            {
                return new SearchResponse() { HasError = true, Error = $"Error to read response\n{e}" };
            }

            var result = ParseSearchResponse(responseBody);

            if (result.HasError == false)
            {
                cts?.Cancel();
            }

            result.SearchSystemId = SearchSystem.DefaultRecord.FirstOrDefault(x => x.SystemName.Equals("google"))?.Id;
            return result;
        }

        public string GetFormattedSearchUrl(string searchText)
        {
            var url = string.Format(
                _googleSearchOption.BaseUrl,
                _googleSearchOption.Cx,
                _googleSearchOption.Key,
                searchText);
            return url;
        }

        public async Task<HttpWebResponse> DoSearch(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();

            return response;
        }

        public string ReadResponseString(HttpWebResponse response)
        {
            using var reader = new StreamReader(response.GetResponseStream());
            var responseBody = reader.ReadToEnd();
            return responseBody;
        }

        public SearchResponse ParseSearchResponse(string responseBody)
        {
            var result = new SearchResponse();

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