using System.Collections.Generic;
using SearchEngines.Web.SearchEngines.Base;

namespace SearchEngines.Web.Util
{
    /// <summary>
    /// Search engines service interface
    /// </summary>
    public interface ISearchEngineServices
    {
        /// <summary>
        /// Collection of implementation of search engine
        /// </summary>
        public ICollection<ISearchEngine> SearchEngines { get; set; }
    }
}