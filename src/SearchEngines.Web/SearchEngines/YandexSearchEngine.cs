using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using SearchEngines.Db.Entities;
using SearchEngines.Web.Base;

namespace SearchEngines.Web.SearchEngines
{
    public class YandexSearchEngine: ISearchEngine
    {
        private readonly YandexSearchOption _yandexSearchOption;
        public YandexSearchEngine(YandexSearchOption yandexSearchOption)
        {
            _yandexSearchOption = yandexSearchOption;
        }

        public  SearchResponse Search(string searchText, CancellationTokenSource cts)
        {
            var url = string.Format(
                _yandexSearchOption.BaseUrl,
                _yandexSearchOption.User,
                _yandexSearchOption.Key,
                searchText);


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var result = ParseSearchResponse(response);

            cts.Cancel();

            return result;
        }

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