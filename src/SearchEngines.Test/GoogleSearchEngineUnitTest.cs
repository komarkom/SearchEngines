using System;
using System.IO;
using NUnit.Framework;
using SearchEngines.Web.SearchEngines.Google;

namespace SearchEngines.Test
{
    public class GoogleSearchEngineUnitTest
    {
        [Test]
        public void GenerateValidUrl()
        {
            var opt = new GoogleSearchOption()
            {
                BaseUrl =
                    "https://www.googleapis.com/customsearch/v1?cx={0}&key={1}&q={2}",
                Key = "key", 
                Cx = "cx"
            };

            var gse = new GoogleSearchEngine(opt);
            var searchText = "Привет";
            var url = gse.GetFormattedSearchUrl(searchText);

            Assert.True(!string.IsNullOrWhiteSpace(url), "url is null or whitespace");
        }

        [Test]
        public void GenerateInvalidUrlWithFormatException()
        {
            var opt = new GoogleSearchOption()
            {
                BaseUrl =
                    "https://www.googleapis.com/customsearch/v1?cx={0}&key=}&q={2}",
                Key = "key",
                Cx = "cx"
            };

            var gse = new GoogleSearchEngine(opt);

            Assert.Throws<FormatException>(() => gse.GetFormattedSearchUrl(null));
        }

        [Test]
        public void GenerateInvalidUrlWithNullReferenceException()
        {
            var gse = new GoogleSearchEngine(null);

            var searchText = "Привет";

            Assert.Throws<NullReferenceException>(() => gse.GetFormattedSearchUrl(searchText));
        }

        [Test]
        public void ParseValidSearchResult()
        {
            string json = File.ReadAllText("./Data/GoogleSuccessResponse.json");

            var gse = new GoogleSearchEngine(null);

            var result = gse.ParseSearchResponse(json);
            Assert.True(result != null && !result.HasError, "JSON response not parsed");
        }

        [Test]
        public void ParseErrorSearchResult()
        {
            string json = File.ReadAllText("./Data/GoogleErrorResponse.json");

            var gse = new GoogleSearchEngine(null);

            var result = gse.ParseSearchResponse(json);
            Assert.True(result != null && result.HasError, "JSON response not parsed");
        }
    }
}