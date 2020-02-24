using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SearchEngines.Db.Entities;
using SearchEngines.Web.SearchEngines;
using SearchEngines.Web.SearchEngines.Base;
using SearchEngines.Web.Util;

namespace SearchEngines.Test
{
    public class Tests
    {
        [Test]
        public void GetSuccessResponse()
        {
            var searchEngineMock = new Mock<ISearchEngine>();
            searchEngineMock.Setup(x => x.Search(It.IsAny<string>(), It.IsAny<CancellationTokenSource>()))
                .Returns((string searchText, CancellationTokenSource cts) => GetFakeSuccessResponse(searchText, cts));

            var searchEngineService = new SearchEngineServices()
            {
                SearchEngines = new List<ISearchEngine>()
                    {searchEngineMock.Object}
            };

            var searchManager = new SearchManager(searchEngineService);

            var result = searchManager.Search("qqq");

            Assert.True(result.IsOk, "Result not ok, when expect ok");
        }

        private Task<SearchResponse> GetFakeSuccessResponse(string searchText, CancellationTokenSource cts)
        {
            return Task.FromResult(new SearchResponse() {HasError = false});
        }

        [Test]
        public void GetErrorResponse()
        {
            var searchEngineMock = new Mock<ISearchEngine>();
            searchEngineMock.Setup(x => x.Search(It.IsAny<string>(), It.IsAny<CancellationTokenSource>()))
                .Returns((string searchText, CancellationTokenSource cts) => GetFakeErrorResponse(searchText, cts));

            var searchEngineService = new SearchEngineServices()
            {
                SearchEngines = new List<ISearchEngine>()
                    {searchEngineMock.Object}
            };

            var searchManager = new SearchManager(searchEngineService);

            var result = searchManager.Search("qqq");

            Assert.True(!result.IsOk, "Result not ok, when expect ok");
        }

        private Task<SearchResponse> GetFakeErrorResponse(string searchText, CancellationTokenSource cts)
        {
            return Task.FromResult(new SearchResponse() { HasError = true, Error = "Fake error" });
        }
    }
}