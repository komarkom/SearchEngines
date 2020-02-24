namespace SearchEngines.Web.SearchEngines.Bing.Models
{
    /// <summary>
    /// Info about found web page
    /// </summary>
    public class Value
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Link to web page
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Preview data
        /// </summary>
        public string Snippet { get; set; }
    }
}