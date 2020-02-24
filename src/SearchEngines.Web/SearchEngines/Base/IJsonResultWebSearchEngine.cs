using System.Net;
using SearchEngines.Db.Entities;

namespace SearchEngines.Web.SearchEngines.Base
{
    /// <summary>
    /// Search engine with response as JSON
    /// </summary>
    public interface IJsonResultWebSearchEngine
    {
        /// <summary>
        /// Read body of response 
        /// </summary>
        /// <param name="response">Search web response</param>
        /// <returns>JSON response body</returns>
        public string ReadResponseString(HttpWebResponse response);

        /// <summary>
        /// Parsing web response with xml format
        /// </summary>
        /// <param name="responseBody">Body of web response</param>
        /// <returns>Search response</returns>
        public SearchResponse ParseSearchResponse(string responseBody);
    }
}