namespace SearchEngines.Web.SearchEngines.Bing.Models
{
    /// <summary>
    /// Model of bing search response
    /// </summary>
    public class BingResponse
    {
        /// <summary>
        /// Inner search result of web page
        /// </summary>
        public WebPages WebPages { get; set; }
    }
}