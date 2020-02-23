namespace SearchEngines.Web.SearchEngines
{
    /// <summary>
    /// Google search settings
    /// </summary>
    public class GoogleSearchOption
    {
        /// <summary>
        /// Base search url for formatting
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Search engine ID
        /// </summary>
        public string Cx { get; set; }

        /// <summary>
        /// API key
        /// </summary>
        public string Key { get; set; }
    }
}