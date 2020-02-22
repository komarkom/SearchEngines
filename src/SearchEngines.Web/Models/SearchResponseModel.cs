using System.Collections.Generic;

namespace SearchEngines.Web.Models
{
    public class SearchResponseModel
    {
        public string Data { get; set; }

        // public virtual SearchSystem SearchSystem { get; set; }

        public virtual ICollection<SearchResultModel> SearchResults { get; set; }
    }
}