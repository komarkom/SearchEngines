using System;
using System.Xml.Linq;
using NUnit.Framework;
using SearchEngines.Web.SearchEngines.Yandex;

namespace SearchEngines.Test
{
    public class YandexSearchEngineUnitTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void GenerateValidUrl()
        {
            var opt = new YandexSearchOption()
            {
                BaseUrl =
                    "https://yandex.com/search/xml?user={0}&key={1}&query={2}&l10n=en&sortby=rlv&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1",
                Key = "key", User = "user"
            };
            var yse = new YandexSearchEngine(opt);
            var searchText = "Привет";
            var url = yse.GetFormattedSearchUrl(searchText);

            Assert.True(!string.IsNullOrWhiteSpace(url), "url is null or whitespace");
        }

        [Test]
        public void GenerateInvalidUrlWithFormatException()
        {
            var opt = new YandexSearchOption()
            {
                BaseUrl =
                    "https://yandex.com/search/xml?user={&key={1}&query={2}&l10n=en&sortby=rlv&filter=none&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1",
            };
            var yse = new YandexSearchEngine(opt);
         
            Assert.Throws<FormatException>(() => yse.GetFormattedSearchUrl(null));
        }

        [Test]
        public void GenerateInvalidUrlWithNullReferenceException()
        {
            var yse = new YandexSearchEngine(null);
            var searchText = "Привет";

            Assert.Throws<NullReferenceException>(() => yse.GetFormattedSearchUrl(searchText));
        }

        [Test]
        public void ParseValidSearchResult()
        {
            XDocument yandexXmlSuccessResponse = XDocument.Load("./Data/YandexSuccessResponse.xml");

            var yse = new YandexSearchEngine(new YandexSearchOption());
            var result = yse.ParseSearchResponse(yandexXmlSuccessResponse);
            Assert.True(result != null && !result.HasError, "XML document not parsed");
        }

        [Test]
        public void ParseErrorSearchResult()
        {
            XDocument yandexErrorResponse = XDocument.Load("./Data/YandexErrorResponse.xml");

            var yse = new YandexSearchEngine(new YandexSearchOption());
            var result = yse.ParseSearchResponse(yandexErrorResponse);
            Assert.True(result != null && result.HasError, "XML document not parsed");
        }
    }
}