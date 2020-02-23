namespace SearchEngines.Web.SearchEngines
{
    /// <summary>
    /// Yandex search settings
    /// </summary>
    public class YandexSearchOption
    {
        /// <summary>
        /// Base search url for formatting
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// API key
        /// </summary>
        public string Key { get; set; }
    }
}