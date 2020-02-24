using System.Collections.Generic;

namespace SearchEngines.Web.SearchEngines.Bing.Models
{
    /// <summary>
    /// Result of web pages
    /// </summary>
    public class WebPages
    {
        /// <summary>
        /// Collection of found web pages
        /// </summary>
        public List<Value> Value { get; set; }
    }
}