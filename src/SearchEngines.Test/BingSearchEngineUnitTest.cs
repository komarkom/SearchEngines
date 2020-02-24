using System;
using System.IO;
using NUnit.Framework;
using SearchEngines.Web.SearchEngines.Bing;

namespace SearchEngines.Test
{
    public class BingSearchEngineUnitTest
    {
        [Test]
        public void GenerateValidUrl()
        {
            var opt = new BingSearchOption()
            {
                BaseUrl =
                    "https://api.cognitive.microsoft.com/bing/v7.0/search?q={0}",
                Key = "key"
            };

            var bse = new BingSearchEngine(opt);
            var searchText = "Привет";
            var url = bse.GetFormattedSearchUrl(searchText);

            Assert.True(!string.IsNullOrWhiteSpace(url), "url is null or whitespace");
        }

        [Test]
        public void GenerateInvalidUrlWithFormatException()
        {
            var opt = new BingSearchOption()
            {
                BaseUrl =
                    "https://api.cognitive.microsoft.com/bing/v7.0/search?q={",
                Key = "key"
            };

            var bse = new BingSearchEngine(opt);

            Assert.Throws<FormatException>(() => bse.GetFormattedSearchUrl(null));
        }

        [Test]
        public void GenerateInvalidUrlWithNullReferenceException()
        {
            var bse = new BingSearchEngine(null);

            var searchText = "Привет";

            Assert.Throws<NullReferenceException>(() => bse.GetFormattedSearchUrl(searchText));
        }

        [Test]
        public void ParseValidSearchResult()
        {
            string json = File.ReadAllText("./Data/BingSuccessResponse.json");

            var bse = new BingSearchEngine(null);

            var result = bse.ParseSearchResponse(json);
            Assert.True(result != null && !result.HasError, "JSON response not parsed");
        }

        [Test]
        public void ParseErrorSearchResult()
        {
            string json = File.ReadAllText("./Data/BingErrorResponse.json");

            var bse = new BingSearchEngine(null);

            var result = bse.ParseSearchResponse(json);
            Assert.True(result != null && result.HasError, "JSON response not parsed");
        }
    }
}