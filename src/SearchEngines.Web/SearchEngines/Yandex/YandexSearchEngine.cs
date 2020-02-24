using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SearchEngines.Db.Entities;
using SearchEngines.Web.SearchEngines.Base;

namespace SearchEngines.Web.SearchEngines.Yandex
{
    /// <summary>
    /// Implementation yandex search engine
    /// </summary>
    ///<inheritdoc cref="ISearchEngine"/>
    ///<inheritdoc cref="IWebRequestSearchEngine"/>
    ///<inheritdoc cref="IXmlResultWebSearchEngine"/>
    public class YandexSearchEngine : ISearchEngine, IWebRequestSearchEngine, IXmlResultWebSearchEngine
    {
        private readonly YandexSearchOption _yandexSearchOption;

        /// <summary>
        /// New instance of YandexSearchEngine
        /// </summary>
        /// <param name="yandexSearchOption">Setting</param>
        public YandexSearchEngine(YandexSearchOption yandexSearchOption)
        {
            _yandexSearchOption = yandexSearchOption;
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

            XDocument responseBody;

            try
            {
                responseBody = ReadResponseXml(response);
            }
            catch (Exception e)
            {
                return new SearchResponse() { HasError = true, Error = $"Error to read xml response\n{e}" };
            }

            var result = ParseSearchResponse(responseBody);

            if (result.HasError == false)
            {
                cts?.Cancel();
            }

            result.SearchSystemId = SearchSystem.DefaultRecord.FirstOrDefault(x => x.SystemName.Equals("yandex"))?.Id;
            return result;
        }

        public string GetFormattedSearchUrl(string searchText)
        {
            var url = string.Format(
                _yandexSearchOption.BaseUrl,
                _yandexSearchOption.User,
                _yandexSearchOption.Key,
                searchText);
            return url;
        }

        public async Task<HttpWebResponse> DoSearch(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();

            return response;
        }

        public XDocument ReadResponseXml(HttpWebResponse response)
        {
            XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
            var xmlResponse = XDocument.Load(xmlReader);
            return xmlResponse;
        }

        public SearchResponse ParseSearchResponse(XDocument xmlResponse)
        {
            var result = new SearchResponse();

            result.Data = xmlResponse.ToString();

            var errorElement = xmlResponse.Elements().Elements("response").Elements("error").FirstOrDefault();

            if (errorElement != null)
            {
                result.HasError = true;
                result.Error = errorElement.Value;

                return result;
            }

            var groupQuery = xmlResponse.Elements()
                .Elements("response")
                .Elements("results")
                .Elements("grouping")
                .Elements("group")
                .Select(gr => gr);

            var xElements = groupQuery as XElement[] ?? groupQuery.ToArray();

            for (int i = 0; i < xElements.Length; i++)
            {
                string url = GetValue(xElements.ElementAt(i), "url");
                string headerLinkText = GetValue(xElements.ElementAt(i), "title");
                string previewData = GetValue(xElements.ElementAt(i), "headline");

                result.SearchResults.Add(new SearchResult()
                    {HeaderLinkText = headerLinkText, Url = url, PreviewData = previewData});
            }

            return result;
        }

        /// <summary>
        /// Get value from xElement
        /// </summary>
        /// <param name="group">Group</param>
        /// <param name="name">Element name</param>
        /// <returns>Value</returns>
        private string GetValue(XElement group, string name)
        {
            try
            {
                return group.Element("doc").Element(name).Value;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}