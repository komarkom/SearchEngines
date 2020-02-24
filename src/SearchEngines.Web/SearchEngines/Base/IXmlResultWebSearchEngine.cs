using System.Net;
using System.Xml.Linq;
using SearchEngines.Db.Entities;

namespace SearchEngines.Web.SearchEngines.Base
{
    /// <summary>
    /// Search engine with response as XML
    /// </summary>
    public interface IXmlResultWebSearchEngine
    {
        /// <summary>
        /// Read body of response 
        /// </summary>
        /// <param name="response">Search web response</param>
        /// <returns>XDocument response body</returns>
        public XDocument ReadResponseXml(HttpWebResponse response);

        /// <summary>
        /// Parsing web response with xml format
        /// </summary>
        /// <param name="xmlResponse">Body of web response</param>
        /// <returns>Search response</returns>
        public SearchResponse ParseSearchResponse(XDocument xmlResponse);
    }
}