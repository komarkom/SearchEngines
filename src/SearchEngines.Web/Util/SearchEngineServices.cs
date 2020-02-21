using System.Collections.Generic;
using SearchEngines.Web.Base;

namespace SearchEngines.Web.Util
{
    public class SearchEngineServices
    {
        public ICollection<ISearchEngine> SearchEngines { get; set; }
    }
}