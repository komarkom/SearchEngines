using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using SearchEngines.Db.Entities;
using SearchEngines.Web.SearchEngines.Base;

namespace SearchEngines.Web.SearchEngines
{
    /// <summary>
    /// Implementation yandex search engine
    /// </summary>
    ///<inheritdoc cref="ISearchEngine"/>
    public class YandexSearchEngine : ISearchEngine
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

        public SearchResponse Search(string searchText, CancellationTokenSource cts)
        {
            var url = string.Format(
                _yandexSearchOption.BaseUrl,
                _yandexSearchOption.User,
                _yandexSearchOption.Key,
                searchText);

            HttpWebResponse response;
            try
            {
                response = SendRequest(url);
            }
            catch (Exception e)
            {
                return new SearchResponse() {HasError = true, Error = e.ToString()};
            }

            var result = ParseSearchResponse(response);

            if (result.HasError == false)
            {
                cts.Cancel();
            }

            result.SearchSystemId = SearchSystem.DefaultRecord.FirstOrDefault(x => x.SystemName.Equals("yandex"))?.Id;
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

            XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
            XDocument xmlResponse = XDocument.Load(xmlReader);

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