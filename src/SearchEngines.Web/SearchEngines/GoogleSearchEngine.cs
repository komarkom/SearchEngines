﻿using System;
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
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly GoogleSearchOption _googleSearchOption;

        public GoogleSearchEngine(GoogleSearchOption googleSearchOption)
        {
            _googleSearchOption = googleSearchOption;
        }

        public SearchResponse Search(string searchText, CancellationTokenSource cts)
        {
            HttpWebResponse response;
            try
            {
                var url = string.Format(
                    _googleSearchOption.BaseUrl,
                    _googleSearchOption.Cx,
                    _googleSearchOption.Key,
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

            result.SearchSystemId = SearchSystem.DefaultRecord.FirstOrDefault(x => x.SystemName.Equals("google"))?.Id;
            return result;
        }

        /// <summary>
        /// Send search request
        /// </summary>
        /// <param name="url">Formatted searching url</param>
        /// <returns>Web response</returns>
        private HttpWebResponse SendRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
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
            GoogleResponse googleResponse;
            string responseBody;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();
            }
            
            googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(responseBody);

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

    public class GoogleResponse
    {
        public SearchInformation SearchInformation { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Snippet { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public class GoogleErrorResponse
    {
        public Error Error { get; set; }
    }

    public class SearchInformation
    {
        public double SearchTime { get; set; }
        public string TotalResults { get; set; }
    }

}