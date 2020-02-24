using System.Net;
using System.Threading.Tasks;

namespace SearchEngines.Web.SearchEngines.Base
{
    /// <summary>
    /// Search engine with web API
    /// </summary>
    public interface IWebRequestSearchEngine
    {
        /// <summary>
        /// Get url to send search request
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <returns>Url</returns>
        public string GetFormattedSearchUrl(string searchText);

        /// <summary>
        /// Send search request and get response
        /// </summary>
        /// <param name="url">Formatted searching url</param>
        /// <returns>Web response</returns>
        public Task<HttpWebResponse> DoSearch(string url);
    }
}