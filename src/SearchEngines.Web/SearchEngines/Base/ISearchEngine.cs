using System.Threading;
using System.Threading.Tasks;
using SearchEngines.Db.Entities;

namespace SearchEngines.Web.SearchEngines.Base
{
    /// <summary>
    /// Search engine interface
    /// </summary>
    public interface ISearchEngine
    {
        /// <summary>
        /// Search user text in searching engine
        /// </summary>
        /// <param name="searchText">User search text</param>
        /// <param name="cts">Cancellation token for cancel request</param>
        /// <returns>Search response</returns>
        public Task<SearchResponse> Search(string searchText, CancellationTokenSource cts);
    }
}