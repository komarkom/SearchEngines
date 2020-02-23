namespace SearchEngines.Web.Util
{
    /// <summary>
    /// Result of any type
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    public class Result<T> where T : class
    {
        /// <summary>
        /// Success status
        /// </summary>
        public bool IsOk { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Result
        /// </summary>
        public T Value { get; set; }
    }
}