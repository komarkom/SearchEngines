using System.Collections.Generic;

namespace SearchEngines.Web.SearchEngines.Google.Model
{
    /// <summary>
    /// Model of google search response
    /// </summary>
    public class GoogleResponse
    {
        /// <summary>
        /// Search information
        /// </summary>
        public SearchInformation SearchInformation { get; set; }

        /// <summary>
        /// Result search items
        /// </summary>
        public List<Item> Items { get; set; }
    }
}