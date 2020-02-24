namespace SearchEngines.Web.SearchEngines.Google.Model
{
    /// <summary>
    /// Google found web page info
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Header link
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Link to web page
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Preview data
        /// </summary>
        public string Snippet { get; set; }
    }
}