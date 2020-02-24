namespace SearchEngines.Web.SearchEngines.Google.Model
{
    /// <summary>
    /// Info about search request
    /// </summary>
    public class SearchInformation
    {
        /// <summary>
        /// Time of searching
        /// </summary>
        public double SearchTime { get; set; }

        /// <summary>
        /// Total results
        /// </summary>
        public string TotalResults { get; set; }
    }
}