namespace SearchEngines.Web.SearchEngines.Google.Model
{
    /// <summary>
    /// Model of google error
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error status
        /// </summary>
        public string Status { get; set; }
    }
}