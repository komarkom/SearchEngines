namespace SearchEngines.Web.SearchEngines
{
    /// <summary>
    /// Bing search settings
    /// </summary>
    public class BingSearchOption
    {
        /// <summary>
        /// Base search url for formatting
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// API key
        /// </summary>
        public string Key { get; set; }
    }
}