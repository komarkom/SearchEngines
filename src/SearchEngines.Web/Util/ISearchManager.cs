using SearchEngines.Db.Entities;
using SearchEngines.Web.Util.Model;

namespace SearchEngines.Web.Util
{
    /// <summary>
    /// Search manager interface
    /// </summary>
    public interface ISearchManager
    {
        /// <summary>
        /// Search user query from search engines
        /// </summary>
        /// <param name="searchText">User query</param>
        /// <returns>Search result with inner search response</returns>
        public Result<SearchResponse> Search(string searchText);
    }
}